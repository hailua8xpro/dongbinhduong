$(document).ready(function () {
    $('#frmlogin').submit(function (e) {
        e.preventDefault();
        $.ajax({
            method: 'POST',
            url: '/cms/AdminUsers/Login',
            data: $(this).serialize(),
            beforeSend: function () {
                showLoading();
            },
            success: function (e) {
                hideLoading();
                loginSuccess(e);
            }
        })
    })
    $('#frmChangePass').submit(function (e) {
        e.preventDefault();
        $.ajax({
            method: 'POST',
            url: '/cms/AdminUsers/ChangePassword',
            data: $(this).serialize(),
            beforeSend: function () {
                showLoading();
            },
            success: function (e) {
                hideLoading();
                if (e._result < 0) {
                    alert(e._error);
                    return;
                }
                alert("Đổi mật khẩu thành công, vui lòng đăng nhập lại");
                location.href = '/cms/AdminUsers/Signout';
            }
        })
    })
})
function loginSuccess(e) {
    if (e._result < 0)
    {
        alert(e._error);
        return;
    }
    var url = getAllUrlParams().returnurl;
    if (url !== undefined) {
       url=url.split("%2f").join("/");
        location.href =url;
        return;
    }
    location.href = "/cms";
}
function changePassSuccess(e) {
    if (e._result < 0) {
        alert(e._error);
        return;
    }
    alert("Đổi mật khẩu thành công, vui lòng đăng nhập lại");
    $.post('/cms/AdminUsers/Signout', null, function () {location.href="/cms" });
}