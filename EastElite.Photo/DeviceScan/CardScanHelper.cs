using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceScan
{
    public class CardScanHelper
    {
        public int icdev; // 通讯设备标识符
        public Int16 st;
        public int sec;
        public string ScanCard()
        {
            string result = "";
            try
            {
                if (Connect())
                {
                    result = ReadCard();
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        Beep();


                    }
                    Disconnect();
                }
            }
            catch {
                return "";
            }
            return result;
        }

        private bool Connect()
        {
            st = 0;
            byte[] ver = new byte[30];
            int[] baudarray = new int[5];
            baudarray[0] = 9600;
            baudarray[1] = 19200;
            baudarray[2] = 38400;
            baudarray[3] = 57600;
            baudarray[4] = 115200;

            st = CardScanCommon.lib_ver(ver);
            string sver = System.Text.Encoding.ASCII.GetString(ver);
      
            icdev = CardScanCommon.Open_USB();
            if (icdev > 0)
            {
                return true;

            }
            else
                return false;

        }
        private bool Disconnect()
        {
            if (CardScanCommon.Close_USB(icdev) == 0)
            { return true; }
            else
                return false;
        }
        private bool Beep()
        {
            if (CardScanCommon.rf_beep(icdev, 10) == 0)
            { return true; }
            else
                return false;
        }
        private string ReadCard()
        {

            UInt16 tagtype = 0;
            byte size = 0;
            uint snr = 0;

            CardScanMain.rf_reset(icdev, 3);
            st = CardScanMain.rf_request(icdev, 1, out tagtype);
            if (st != 0)
            {

                return "";
            }

            st = CardScanMain.rf_anticoll(icdev, 0, out snr);
            if (st != 0)
            {

                return "";
            }
            string snrstr = "";
            snrstr = snr.ToString("X");


            st = CardScanMain.rf_select(icdev, snr, out size);
            if (st != 0)
            {

                return "";
            }
            return snrstr;

        }
    }
}
