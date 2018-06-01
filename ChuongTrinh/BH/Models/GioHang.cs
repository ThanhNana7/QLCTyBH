using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BH.Models;

namespace BH.Models
{
    public class Giohang
    {
        dbBachHoa db = new dbBachHoa();

        public int iMSMH { set; get; }
        public string sTenHang { set; get; }
        public string sHinhAnh { set; get; }
        public Double dDonGia { set; get; }
        public int iSoLuong { set; get; }
        public Double dThanhtien
        {
            get { return iSoLuong * dDonGia; }
        }
        //Khởi tạo giỏ hàng theo Masach được truyền vào với Soluong mặc định là 1

        public Giohang(int MSMH)
        {
            iMSMH = MSMH;
            MatHang mathang = db.MatHangs.Single(n => n.MSMH == iMSMH);
            sTenHang = mathang.TenHang;
            sHinhAnh = mathang.HinhAnh;
            dDonGia = double.Parse(mathang.DonGia.ToString());
            iSoLuong = 1;
        }
    }
}