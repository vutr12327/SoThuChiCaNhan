using SoThuChiCaNhan.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

/*
 * 
 *  DỊCH VỤ
 * 
*/

public partial class frm_GiaoDienChinh
{
    private readonly srv_NguoiDung DichVuNguoiDung = new();
    private readonly srv_DanhMuc DichVuDanhMuc = new();
    private readonly srv_GiaoDich DichVuGiaoDich = new();
}
