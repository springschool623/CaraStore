using CaraLuggage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CaraLuggage.Controllers.Visitor
{
    public class JsonExportVisitor : IVisitor
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        private List<object> donHangs;

        public JsonExportVisitor() 
        {
            donHangs = new List<object>();
        }
        public void VisitDonHang(DonHang donHang)
        {
            var chiTietDonHangs = db.ChiTietDonHangs.Where(p => p.od_orderno == donHang.order_no).ToList();
            foreach(var chiTietDonHang in chiTietDonHangs)
            {
                if(chiTietDonHang != null)
                {
                    var info = $"order_id: {donHang.order_no}, order_detail: {{ price: {chiTietDonHang.od_price}, product: {chiTietDonHang.od_product} }}";
                    donHangs.Add(info);
                }
                else
                {
                    var info = $"order_id: {donHang.order_no}, order_detail: {{ price: N/A, product: N/A }}";
                    donHangs.Add(info);
                }
            }
        }

        public void SaveToJson()
        {
            string fileName = @"D:\test.txt";
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(directory, fileName);

            //Mở một StreamWriter để ghi vào file văn bản
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach(var donHang in donHangs)
                {
                    writer.WriteLine(donHang.ToString()); //Ghi mỗi đơn hàng vào một dòng.
                }
            }
        }
    }
}