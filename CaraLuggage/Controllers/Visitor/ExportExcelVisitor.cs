using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace CaraLuggage.Controllers.Visitor
{
    public class ExportExcelVisitor
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        private List<object> chiTietDonHangs, donHangs;
        private ChiTietDonHang chiTietDonHang;

        public ExportExcelVisitor()
        {
            donHangs = new List<object>();
            chiTietDonHangs = new List<object>();
        }

        public void VisitDonHang(DonHang donHang)
        {
            var ChiTietDonHangs = db.ChiTietDonHangs.Where(s => s.od_orderno == donHang.order_no).ToList();

            var info_donHang = new
            {
                orderCode = donHang.order_code,
                orderCreateAt = donHang.order_createAt,
                orderTotalPrice = donHang.order_totalPrice,
                orderCustomer = donHang.KhachHang.customer_name,
                orderStaff = donHang.order_staff,
                orderPayment = donHang.PhuongThucThanhToan.payment_name,
                orderKHAddress = donHang.KhachHang.customer_address,
                orderKHPhone = donHang.KhachHang.customer_phone
            };

            foreach(var chiTietDonHang in ChiTietDonHangs)
            {
                var info_chiTietDonHang = new
                {
                    orderDetailId = chiTietDonHang.orderdetail_id,
                    orderDetailProduct = chiTietDonHang.SanPham.product_name,
                    orderDetailQuantity = chiTietDonHang.od_quantity,
                    orderDetailPrice = chiTietDonHang.od_price,
                    orderDetailNo = chiTietDonHang.od_orderno
                };
                chiTietDonHangs.Add(info_chiTietDonHang);
            }

            donHangs.Add(info_donHang);
        }

        public void SaveToExcel()
        {
            // Khai báo các đối tượng Excel
            Excel.Application dataApp = new Excel.Application();
            Excel.Workbook dataWorkbook = dataApp.Workbooks.Open(@"E:\MVC-Web\CaraStore\CaraLuggage\Template\VisitorOrder.xlsx");
            Excel.Worksheet dataWorksheet = dataWorkbook.Sheets[1];
            Excel.Range xlRange = dataWorksheet.UsedRange;

            ClearPreviousData(xlRange);

            int row = 9; //Dòng bắt đầu thêm dữ liệu mới
            foreach (var chiTietDonHang in chiTietDonHangs)
            {
                var info_chiTietDonHang = (dynamic)chiTietDonHang;
                xlRange.Cells[row, 2].Value = info_chiTietDonHang.orderDetailProduct;
                xlRange.Cells[row, 3].Value = info_chiTietDonHang.orderDetailQuantity;
                xlRange.Cells[row, 4].Value = info_chiTietDonHang.orderDetailPrice;
                row++;
            }
            foreach (var donHang in donHangs)
            {
                var info_donHang = (dynamic)donHang;
                xlRange.Cells[4, 2].Value = info_donHang.orderCustomer;
                xlRange.Cells[5, 2].Value = info_donHang.orderKHAddress;
                xlRange.Cells[6, 2].Value = info_donHang.orderKHPhone;
                xlRange.Cells[24, 3].Value = info_donHang.orderTotalPrice;

            }
            dataWorkbook.Save(); // Lưu thay đổi
            dataWorkbook.Close(false); // Đóng workbook
            dataApp.Quit(); // Đóng ứng dụng Excel
        }

        private void ClearPreviousData(Excel.Range xlRange)
        {
            // Kiểm tra nếu ô không null trước khi xóa
            if (xlRange.Cells[9, 2].Value != null && xlRange.Cells[9, 3].Value != null && xlRange.Cells[9, 4].Value != null)
            {
                // Xóa dữ liệu từ dòng 9 đến dòng 24 và từ cột 2 đến cột 4
                xlRange.Range[xlRange.Cells[9, 2], xlRange.Cells[23, 4]].ClearContents();
            }
            // Kiểm tra nếu ô không null trước khi xóa
            if (xlRange.Cells[4, 2].Value != null && xlRange.Cells[5, 2].Value != null && xlRange.Cells[6, 2].Value != null && xlRange.Cells[24, 3].Value != null)
            {
                // Xóa dữ liệu từ dòng 4 đến dòng 6 và cột 2
                xlRange.Range[xlRange.Cells[4, 2], xlRange.Cells[6, 2]].ClearContents();
            }
        }
    }
}
