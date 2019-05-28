/// <reference path="jquery-2.0.0.js" />
/// <reference path="jquery-2.0.0.intellisense.js" />

$(document).ready(function () {
    $('#frm-register').submit(function (e) {
        e.preventDefault();
        if ($('#ConfirmPassword').val()!==$('#Password').val()) {
            alert("Mật khẩu không trùng khớp");
            $('#ConfirmPassword').focus();
            return false;
        }
        var data = $(this).serialize();
        var url = "/aj/account/register";
        $.ajax({
            data: data,
            url: url,
            method: 'POST',
            beforeSend: function () { showLoading(); },
            success: function (e) {
                hideLoading();
                if (e._result<1) {
                    alert(e._error);
                }
                else {
                    $('#frm-register').fadeOut();
                    $('.cpsuccess').fadeIn();
                    setTimeout(function () {
                        location.href = "/dang-nhap";
                    }, 4000)
                }
            },
            error: function () {
                hideLoading();
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        })
    })
    $('#frm-login').submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        var url = "/aj/account/login";
        $.ajax({
            data: data,
            url: url,
            method: 'POST',
            beforeSend: function () { showLoading(); },
            success: function (e) {
                hideLoading();
                if (e._result < 1) {
                    alert(e._error);
                }
                else {
                    location.href = "/";
                }
            },
            error: function () {
                hideLoading();
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        })
    })
    $('#frmuserinfo').submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        var url = "/aj/account/UpdateMyAccount";
        $.ajax({
            data: data,
            url: url,
            method: 'POST',
            beforeSend: function () { showLoading(); },
            success: function (e) {
                hideLoading();
                if (e._result < 1) {
                    alert(e._error);
                }
                else {
                    alert("Cập nhật thông tin thành công")
                    location.reload();
                }
            },
            error: function () {
                hideLoading();
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        })
    })
    $('#frm-changepass').submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        var url = "/aj/account/ChangePassword";
        $.ajax({
            data: data,
            url: url,
            method: 'POST',
            beforeSend: function () { showLoading(); },
            success: function (e) {
                hideLoading();
                if (e._result < 1) {
                    alert(e._error);
                }
                else {
                    alert("Đổi mật khẩu thành công, vui lòng đăng nhập");
                    location.href = "/dang-nhap";
                }
            },
            error: function () {
                hideLoading();
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        })
    })
    $('#frm-forgotpass').submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        var url = "/aj/account/ResetPass";
        $.ajax({
            data: data,
            url: url,
            method: 'POST',
            beforeSend: function () { showLoading(); },
            success: function (e) {
                hideLoading();
                if (e._result < 1) {
                    alert(e._error);
                }
                else {
                    alert("Vui lòng kiểm tra email của bạn để nhận lại mật khẩu");
                    location.href = "/dang-nhap";
                }
            },
            error: function () {
                hideLoading();
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        })
    })
})