using SoThuChiCaNhan.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class uc_TroLyAI
{
    srv_NguoiDung DichVuNguoiDung = new();
    srv_GiaoDich DichVuGiaoDich = new();
    srv_DanhMuc DichVuDanhMuc = new();
    srv_ChatBoxAI DichVuAI = new();
}
