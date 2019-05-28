/// <reference path="jquery-2.0.0.js" />
/// <reference path="jquery-2.0.0.intellisense.js" />

function updateCart() {
    var lstcartitem = [];
    var obj = {};
    $('.cartitem').each(function () {
        var id = $(this).data('id');
        var qty = $(this).find('.cart-q').val();
        obj = { ProductId: id, Quantity: qty};
        lstcartitem.push(obj);
    })
    var data = JSON.stringify({ 'lstCartUpdateItem': lstcartitem })
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: "/aj/order/updatecart",
        data: data,
        method: 'POST',
        beforeSend: function () { showLoading() },
        success: function (e) {
            hideLoading();
            if (e._result == 1) {
                $('#wrcart').replaceWith(e._html);
            }
            else {
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        },
        error: function (e) {
            hideLoading();
            alert("Có lỗi xảy ra, vui lòng thử lại");
        }
    })
}
function removeFromCart(id) {
    var url = "/aj/order/RemoveFromCart"
    $.ajax({
        url: url,
        data: { id: id},
        beforeSend: function () { showLoading() },
        success: function (e) {
            hideLoading();
            if (e._result > 0) {
                $('#wrcart').replaceWith(e._view);
            }
            else {
                alert("Có lỗi xảy ra, vui lòng thử lại");
            }
        },
        error: function (e) {
            hideLoading();
            alert("Có lỗi xảy ra, vui lòng thử lại");
        }
    })
}
$(document).on('change', '.cart-q', function () {
    updateCart();
})
$(document).ready(function () {
    $('#order-form').submit(function (e) {
        e.preventDefault();
        if (!flg) {
            flg = true;
            return false;
        }
        flg = false;

        var url = "/aj/order/SubmitOrder";
        $.ajax({
            url: url,
            method: 'POST',
            data: new FormData(this),
            processData: false,
            contentType: false,
            beforeSend: function () {
                showLoading();
            },
            success: function (e) {
                flg = true;
                hideLoading();
                if (e._result > 0) {
                    $('#order-form').css("visibility", "hidden");
                    $('.ordersucess').show();
                }
                else {
                    alert(e._error);
                }
                $('#order-form').trigger('reset');
            }
        })
    })

    $('.orderid').click(function () {
        var id = $(this).data('orderid');
        if (id>0) {
            location.href = "/chi-tiet-don-hang?id=" + id;
        }
    })
})
$('#frmorderlogin').submit(function (e) {
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
                location.href = "/don-hang";
            }
        },
        error: function () {
            hideLoading();
            alert("Có lỗi xảy ra, vui lòng thử lại");
        }
    })
})