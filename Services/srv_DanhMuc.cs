using System.Collections.Generic;

namespace SoThuChiCaNhan;

public enum srv_KetQuaDanhMuc
{
    ThanhCong,
    DuLieuKhongHopLe,
    ThatBai
}

public class srv_DanhMuc
{
    private readonly dal_DanhMuc dalDanhMuc = new dal_DanhMuc();

    public List<mod_DanhMuc> LayDanhSachDanhMuc()
    {
        return dalDanhMuc.LayTatCaDanhMuc();
    }

    public srv_KetQuaDanhMuc ThemDanhMuc(string tenDanhMuc, string loai)
    {
        if (!KiemTraDuLieu(tenDanhMuc, loai))
        {
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;
        }

        bool ketQua = dalDanhMuc.ThemDanhMuc(tenDanhMuc.Trim(), loai);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    public srv_KetQuaDanhMuc SuaDanhMuc(int maDanhMuc, string tenDanhMuc, string loai)
    {
        if (maDanhMuc <= 0 || !KiemTraDuLieu(tenDanhMuc, loai))
        {
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;
        }

        bool ketQua = dalDanhMuc.SuaDanhMuc(maDanhMuc, tenDanhMuc.Trim(), loai);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    public srv_KetQuaDanhMuc XoaDanhMuc(int maDanhMuc)
    {
        if (maDanhMuc <= 0)
        {
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;
        }

        bool ketQua = dalDanhMuc.XoaDanhMuc(maDanhMuc);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    private bool KiemTraDuLieu(string tenDanhMuc, string loai)
    {
        return !string.IsNullOrWhiteSpace(tenDanhMuc) &&
               tenDanhMuc.Trim().Length <= 100 &&
               (loai == "Thu" || loai == "Chi");
    }
}
