using S7.Net;
using S7.Net.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S7Example
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var plc = new Plc(CpuType.S71200, "127.0.0.1", 0, 1))
            {
                var result = plc.Open();
                if (result != ErrorCode.NoError)
                {
                    Console.WriteLine("Error: " + plc.LastErrorCode + "\n" + plc.LastErrorString);
                }
                else
                {
                    // 1: example on how to read variables one by one by specifing the absolute address
                    ReadSingleVariables(plc);

                    // 2: example on how to read block of bytes + conversions
                    //ReadBytes(plc);

                    // 3: example on how to read classes 
                    //ReadClasses(plc);

                    // 4: example on how to read multiple block of bytes in a single request
                    //ReadMultipleVars(plc);

                    // 5: clears the db values by writing two array bytes full of 0
                    //ClearDbValues(plc);

                    // 6: example on how to write varbiables one by one by specifing the absolute address
                    //WriteSingleVariable(plc);

                    // 7: example on how to write block of bytes 
                    //WriteBytes(plc);
                }
            }
            Console.WriteLine("\nPress a key to continue...");
            Console.ReadKey();
        }

        private static void ReadMultipleVars(Plc plc)
        {
            List<DataItem> dataItems = new List<DataItem>
                {
                    new DataItem()
                    {
                        DataType = DataType.DataBlock,
                        DB = 1,
                        Count = 38,
                    },
                    new DataItem()
                    {
                        DataType = DataType.DataBlock,
                        DB = 3,
                        Count = 18,
                    }
                };

            plc.ReadMultipleVars(dataItems);

            Console.WriteLine("\n--- DB 1 ---\n");

            var db1Bytes = dataItems[0].Value as byte[];

            bool db1Bool1 = db1Bytes[0].SelectBit(0);
            Console.WriteLine("DB1.DBX0.0: " + db1Bool1);

            bool db1Bool2 = db1Bytes[0].SelectBit(1);
            Console.WriteLine("DB1.DBX0.1: " + db1Bool2);

            short db1IntVariable = S7.Net.Types.Int.FromByteArray(db1Bytes.Skip(2).Take(2).ToArray());
            Console.WriteLine("DB1.DBW2.0: " + db1IntVariable);

            double db1RealVariable = S7.Net.Types.Double.FromByteArray(db1Bytes.Skip(4).Take(4).ToArray());
            Console.WriteLine("DB1.DBD4.0: " + db1RealVariable);

            int db1DintVariable = S7.Net.Types.DInt.FromByteArray(db1Bytes.Skip(8).Take(4).ToArray());
            Console.WriteLine("DB1.DBD8.0: " + db1DintVariable);

            uint db1DwordVariable = S7.Net.Types.DWord.FromByteArray(db1Bytes.Skip(12).Take(4).ToArray());
            Console.WriteLine("DB1.DBD12.0: " + db1DwordVariable);

            ushort db1WordVariable = S7.Net.Types.Word.FromByteArray(db1Bytes.Skip(16).Take(2).ToArray());
            Console.WriteLine("DB1.DBW16.0: " + db1WordVariable);


            Console.WriteLine("\n--- DB 3 ---\n");

            var db3Bytes = dataItems[1].Value as byte[];

            bool db3Bool1 = db3Bytes[0].SelectBit(0);
            Console.WriteLine("DB1.DBX0.0: " + db3Bool1);

            bool db3Bool2 = db3Bytes[0].SelectBit(1);
            Console.WriteLine("DB1.DBX0.1: " + db3Bool2);

            short db3_intVariable = S7.Net.Types.Int.FromByteArray(db3Bytes.Skip(2).Take(2).ToArray());
            Console.WriteLine("DB1.DBW2.0: " + db3_intVariable);

            double db3RealVariable = S7.Net.Types.Double.FromByteArray(db3Bytes.Skip(4).Take(4).ToArray());
            Console.WriteLine("DB1.DBD4.0: " + db3RealVariable);

            int db3DintVariable = S7.Net.Types.DInt.FromByteArray(db3Bytes.Skip(8).Take(4).ToArray());
            Console.WriteLine("DB1.DBD8.0: " + db3DintVariable);

            uint db3DwordVariable = S7.Net.Types.DWord.FromByteArray(db3Bytes.Skip(12).Take(4).ToArray());
            Console.WriteLine("DB1.DBD12.0: " + db3DwordVariable);

            ushort db3WordVariable = S7.Net.Types.Word.FromByteArray(db3Bytes.Skip(16).Take(2).ToArray());
            Console.WriteLine("DB1.DBW16.0: " + db3WordVariable);
        }

        private static void ReadClasses(Plc plc)
        {
            Console.WriteLine("\n--- DB 1 ---\n");

            var db1 = new Db1();
            plc.ReadClass(db1, 1);

            Console.WriteLine("DB1.DBX0.0: " + db1.Bool1);
            Console.WriteLine("DB1.DBX0.1: " + db1.Bool2);
            Console.WriteLine("DB1.DBW2.0: " + db1.IntVariable);
            Console.WriteLine("DB1.DBD4.0: " + db1.RealVariable);
            Console.WriteLine("DB1.DBD8.0: " + db1.DintVariable);
            Console.WriteLine("DB1.DBD12.0: " + db1.DwordVariable);
            Console.WriteLine("DB1.DBW16.0: " + db1.WordVariable);

            Console.WriteLine("\n--- DB 3 ---\n");

            var db3 = new Db3();
            plc.ReadClass(db3, 3);

            Console.WriteLine("DB3.DBX0.0: " + db3.Bool1);
            Console.WriteLine("DB3.DBX0.1: " + db3.Bool2);
            Console.WriteLine("DB3.DBW2.0: " + db3.IntVariable);
            Console.WriteLine("DB3.DBD4.0: " + db3.RealVariable);
            Console.WriteLine("DB3.DBD8.0: " + db3.DintVariable);
            Console.WriteLine("DB3.DBD12.0: " + db3.DwordVariable);
            Console.WriteLine("DB3.DBW16.0: " + db3.WordVariable);
        }

        private static void ReadBytes(Plc plc)
        {
            Console.WriteLine("\n--- DB 1 ---\n");

            var db1Bytes = plc.ReadBytes(DataType.DataBlock, 1, 0, 38);            

            bool db1Bool1 = db1Bytes[0].SelectBit(0);
            Console.WriteLine("DB1.DBX0.0: " + db1Bool1);

            bool db1Bool2 = db1Bytes[0].SelectBit(1);
            Console.WriteLine("DB1.DBX0.1: " + db1Bool2);

            short db1IntVariable = S7.Net.Types.Int.FromByteArray(db1Bytes.Skip(2).Take(2).ToArray());
            Console.WriteLine("DB1.DBW2.0: " + db1IntVariable);

            double db1RealVariable = S7.Net.Types.Double.FromByteArray(db1Bytes.Skip(4).Take(4).ToArray());
            Console.WriteLine("DB1.DBD4.0: " + db1RealVariable);

            int db1DintVariable = S7.Net.Types.DInt.FromByteArray(db1Bytes.Skip(8).Take(4).ToArray());
            Console.WriteLine("DB1.DBD8.0: " + db1DintVariable);

            uint db1DwordVariable = S7.Net.Types.DWord.FromByteArray(db1Bytes.Skip(12).Take(4).ToArray());
            Console.WriteLine("DB1.DBD12.0: " + db1DwordVariable);

            ushort db1WordVariable = S7.Net.Types.Word.FromByteArray(db1Bytes.Skip(16).Take(2).ToArray());
            Console.WriteLine("DB1.DBW16.0: " + db1WordVariable);

            Console.WriteLine("\n--- DB 3 ---\n");

            var db3Bytes = (byte[])plc.Read(DataType.DataBlock, 3, 0, VarType.Byte, 18);            

            bool db3Bool1 = db3Bytes[0].SelectBit(0);
            Console.WriteLine("DB1.DBX0.0: " + db3Bool1);

            bool db3Bool2 = db3Bytes[0].SelectBit(1);
            Console.WriteLine("DB1.DBX0.1: " + db3Bool2);

            short db3_intVariable = S7.Net.Types.Int.FromByteArray(db3Bytes.Skip(2).Take(2).ToArray());
            Console.WriteLine("DB1.DBW2.0: " + db3_intVariable);

            double db3RealVariable = S7.Net.Types.Double.FromByteArray(db3Bytes.Skip(4).Take(4).ToArray());
            Console.WriteLine("DB1.DBD4.0: " + db3RealVariable);

            int db3DintVariable = S7.Net.Types.DInt.FromByteArray(db3Bytes.Skip(8).Take(4).ToArray());
            Console.WriteLine("DB1.DBD8.0: " + db3DintVariable);

            uint db3DwordVariable = S7.Net.Types.DWord.FromByteArray(db3Bytes.Skip(12).Take(4).ToArray());
            Console.WriteLine("DB1.DBD12.0: " + db3DwordVariable);

            ushort db3WordVariable = S7.Net.Types.Word.FromByteArray(db3Bytes.Skip(16).Take(2).ToArray());
            Console.WriteLine("DB1.DBW16.0: " + db3WordVariable);
        }

        private static void ReadSingleVariables(Plc plc)
        {
            Console.WriteLine("\n--- DB 1 ---\n");

            bool db1Bool1 = (bool)plc.Read("DB1.DBX0.0");
            Console.WriteLine("DB1.DBX0.0: " + db1Bool1);

            bool db1Bool2 = (bool)plc.Read("DB1.DBX0.1");
            Console.WriteLine("DB1.DBX0.1: " + db1Bool2);

            var db1IntVariable = ((ushort)plc.Read("DB1.DBW2.0")).ConvertToShort();
            Console.WriteLine("DB1.DBW2.0: " + db1IntVariable);

            var db1RealVariable = ((uint)plc.Read("DB1.DBD4.0")).ConvertToDouble();
            Console.WriteLine("DB1.DBD4.0: " + db1RealVariable);

            var db1DintVariable = ((uint)plc.Read("DB1.DBD8.0")).ConvertToInt();
            Console.WriteLine("DB1.DBD8.0: " + db1DintVariable);

            var db1DwordVariable = ((uint)plc.Read("DB1.DBD12.0")).ConvertToInt();
            Console.WriteLine("DB1.DBD12.0: " + db1DwordVariable);

            var db1WordVariable = ((ushort)plc.Read("DB1.DBW16.0")).ConvertToShort();
            Console.WriteLine("DB1.DBW16.0: " + db1WordVariable);

            var db1T1Time = (uint)plc.Read("DB1.DBD26.0");
            Console.WriteLine("DB1.DBD26.0: " + db1T1Time);

            Console.WriteLine("\n--- DB 3 ---\n");

            var db3Bool1 = (bool)plc.Read("DB3.DBX0.0");
            Console.WriteLine("DB3.DBX0.0: " + db3Bool1);

            var db3Bool2 = (bool)plc.Read("DB3.DBX0.1");
            Console.WriteLine("DB3.DBX0.1: " + db3Bool2);

            var db3IntVariable = ((ushort)plc.Read("DB3.DBW2.0")).ConvertToShort();
            Console.WriteLine("DB3.DBW2.0: " + db3IntVariable);

            var db3RealVariable = ((uint)plc.Read("DB3.DBD4.0")).ConvertToDouble();
            Console.WriteLine("DB3.DBD4.0: " + db3RealVariable);

            var db3DintVariable = ((uint)plc.Read("DB3.DBD8.0")).ConvertToInt();
            Console.WriteLine("DB3.DBD8.0: " + db3DintVariable);

            var db3DwordVariable = ((uint)plc.Read("DB3.DBD12.0")).ConvertToInt();
            Console.WriteLine("DB3.DBD12.0: " + db3DwordVariable);

            var db3WordVariable = ((ushort)plc.Read("DB3.DBW16.0")).ConvertToShort();
            Console.WriteLine("DB3.DBW16.0: " + db3WordVariable);
        }

        private static void WriteSingleVariable(Plc plc)
        {
            Console.WriteLine("\n--- DB 1 ---\n");

            plc.Write("DB1.DBX0.0", true);            

            plc.Write("DB1.DBX0.1", true);

            short db1IntVariable = 50;
            plc.Write("DB1.DBW2.0", db1IntVariable.ConvertToUshort());


            double db1RealVariable = 25.36;
            plc.Write("DB1.DBD4.0", db1RealVariable.ConvertToUInt());


            int db1DintVariable = 123456;
            plc.Write("DB1.DBD8.0",db1DintVariable.ConvertToUInt());


            int db1DwordVariable = 123456;
            plc.Write("DB1.DBD12.0", db1DwordVariable.ConvertToUInt());


            short db1WordVariable = 12345;
            plc.Write("DB1.DBW16.0",db1WordVariable.ConvertToUshort());
            

            Console.WriteLine("\n--- DB 3 ---\n");

            plc.Write("DB3.DBX0.0", true);            

            plc.Write("DB3.DBX0.1", true);


            short db3IntVariable = -50;
            plc.Write("DB3.DBW2.0", db3IntVariable.ConvertToUshort());


            double db3RealVariable = -25.36;
            plc.Write("DB3.DBD4.0", db3RealVariable.ConvertToUInt());


            int db3DintVariable = -123456;
            plc.Write("DB3.DBD8.0", db3DintVariable.ConvertToUInt());


            int db3DwordVariable = -123456;
            plc.Write("DB3.DBD12.0", db3DwordVariable.ConvertToUInt());


            short db3WordVariable = -1234;
            plc.Write("DB3.DBW16.0", db3WordVariable.ConvertToUshort());
            
        }

        private static void WriteBytes(Plc plc)
        {
            Console.WriteLine("\n--- DB 1 ---\n");

            byte[] db1Bytes = new byte[18];

            S7.Net.Types.Boolean.SetBit(db1Bytes[0], 0); // DB1.DBX0.0

            S7.Net.Types.Boolean.SetBit(db1Bytes[0], 1); // DB1.DBX0.1

            short db1IntVariable = 50;
            S7.Net.Types.Int.ToByteArray(db1IntVariable).CopyTo(db1Bytes, 2); // DB1.DBW2.0


            double db1RealVariable = 25.36;
            S7.Net.Types.Double.ToByteArray(db1RealVariable).CopyTo(db1Bytes, 4); // DB3.DBD4.0


            int db1DintVariable = 123456;
            S7.Net.Types.DInt.ToByteArray(db1DintVariable).CopyTo(db1Bytes, 8); // DB1.DBD8.0


            uint db1DwordVariable = 123456;
            S7.Net.Types.DWord.ToByteArray(db1DwordVariable).CopyTo(db1Bytes, 12); // DB1.DBD12.0


            ushort db1WordVariable = 12345;
            S7.Net.Types.Word.ToByteArray(db1WordVariable).CopyTo(db1Bytes, 16); // DB1.DBD16.0

            plc.WriteBytes(DataType.DataBlock, 1, 0, db1Bytes);


            Console.WriteLine("\n--- DB 3 ---\n");

            byte[] db3Bytes = new byte[18];

            S7.Net.Types.Boolean.SetBit(db3Bytes[0], 0); // DB3.DBX0.0

            S7.Net.Types.Boolean.SetBit(db3Bytes[0], 1); // DB3.DBX0.1


            short db3IntVariable = -50;
            S7.Net.Types.Int.ToByteArray(db3IntVariable).CopyTo(db3Bytes, 2); // DB3.DBW2.0


            double db3RealVariable = -25.36;
            S7.Net.Types.Double.ToByteArray(db3RealVariable).CopyTo(db3Bytes, 4); // DB3.DBD4.0


            int db3DintVariable = -123456;
            S7.Net.Types.DInt.ToByteArray(db3DintVariable).CopyTo(db3Bytes, 8); // DB3.DBD8.0


            int db3DwordVariable = -123456;
            S7.Net.Types.DWord.ToByteArray(db3DwordVariable.ConvertToUInt()).CopyTo(db3Bytes, 12); // DB3.DBD12.0


            short db3WordVariable = -1234;
            S7.Net.Types.Word.ToByteArray(db3WordVariable.ConvertToUshort()).CopyTo(db3Bytes, 16); // DB3.DBD16.0

            plc.WriteBytes(DataType.DataBlock, 3, 0, db3Bytes);
        }

        private static void ClearDbValues(Plc plc)
        {
            byte[] db1Bytes = new byte[18];
            byte[] db3Bytes = new byte[18];

            plc.WriteBytes(DataType.DataBlock, 1, 0, db1Bytes);
            plc.WriteBytes(DataType.DataBlock, 3, 0, db3Bytes);
        }      
    }
}
