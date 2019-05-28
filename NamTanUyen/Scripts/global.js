/// <reference path="jquery-2.0.0.intellisense.js" />
/// <reference path="jquery-2.0.0.js" />
var flg = true;
function showLoading() {
    $('#dlding').fadeIn();
}

function hideLoading() {
    $('#dlding').fadeOut();
}
function addToCart(id,qty) {
    $.ajax({
        url: "/aj/Order/AddToCart",
        data: { id: id, qty: qty },
        beforeSend: function () { showLoading(); },
        success: function (e) {
            if (e._result > 0) {
                location.href = "/gio-hang";
            }
            else {
                hideLoading();
                alert(e._error);
            }
        }
    })
}
$(document).ready(function () {
    $('.searchlink').click(function () {
        $('.right-nav').addClass('open');
        $('.search-input').focus();
    })
    $('.closesearch').click(function () {
        $('.right-nav').removeClass('open');
    })
    $(document).mouseup(function (e) {
        var subject = $("#right-nav");
        if (e.target.id != subject.attr('id') && !subject.has(e.target).length) {
            $('.right-nav').removeClass('open');
        }
    });
    $('.btnmenu').click(function () {
        $(this).toggleClass('mbopen');
        $("html, body").animate({ scrollTop: 0 }, "slow");
        var top = parseInt($('.social-nav').offset().top);
        $('.mobilemenu').css("height", top);
        $('.mobilemenu').toggleClass('menuopen');

    })
    //$('.ulpomobile > li').click(function () {
    //    $('.ulpomobile > li.active').removeClass("active");
    //    $(this).addClass("active");
    //})

    $('#signout').click(function () {
        var url = "/ajax/account/signout";
        $.ajax({
            url: url,
            success: function () {
                location.href = "/";
            }
        })
    })
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
                    ShowPopUp("Gửi tin nhắn thành công", "Cảm ơn bạn đã liên hệ, chúng tôi sẽ gọi lại ngay");
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
function ShowPopUp(title,content) {
    $('h5.modal-title').text(title);
    $('.modal-body p').text(content);
    $('#messageModel').modal();
    setTimeout(function () { $('#messageModel').modal('hide'); }, 3000);
}
function AddViewCount(note) {
    var url = location.href;
    $.ajax({
        url: "/aj/Common/AddViewCount",
        data: { url: url,note:note}
    })
}
//$(window).scroll(function () {
//    var totop = $(document).scrollTop();
//    if (totop >= hrpos) {
//        $('.gototop').fadeIn();
//    }
//    else {
//        $('.gototop').fadeOut();
//    }

//})