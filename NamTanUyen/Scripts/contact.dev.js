/// <reference path="jquery-2.0.0.intellisense.js" />
/// <reference path="jquery-2.0.0.js" />
$(document).ready(function () {
    $('#frmContact').submit(function (e) {
        e.preventDefault();
        if (!flg) {
            return false;
        }
        flg = false;
        var data = $(this).serialize();
        $.ajax({
            method: 'POST',
            url: "/aj/Common/SubmitContact",
            data: data,
            beforeSend: function () {
                showLoading();
            },
            success: function (e) {
                flg = true;
                hideLoading();
                if (e._result === 1) {
                    alert("Cảm ơn bạn đã liên hệ, nhân viên của chúng tôi sẽ hồi đáp trong thời gian sớm nhất");
                    $('#frmContact').trigger('reset');
                }
                else {
                    alert("Có lỗi xảy ra, vui lòng thử lại");
                    flg = true;
                }
            },
            error: function (e) {
                flg = true;
                hideLoading();
            }
        });
    })
    
})