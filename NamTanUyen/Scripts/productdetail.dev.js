/// <reference path="jquery-2.0.0.intellisense.js" />
/// <reference path="jquery-2.0.0.js" />

$(document).ready(function () {
    $('.addtocart').click(function (e) {
        var qty = $('#Quantity').val();
        if (qty<1) {
            alert("Bạn vui lòng chọn ít nhất 1 sản phẩm");
            return false;
        }
        $.ajax({
            url: "/aj/Order/AddToCart",
            data: { id: productid, qty: qty },
            beforeSend: function () { showLoading(); },
            success: function (e) {
                if (e._result>0) {
                    location.href = "/gio-hang";
                }
                else {
                    hideLoading();
                    alert(e._error);
                }
            }
        })
    })
})

function addToFavorite(id) {
    var url = "/aj/account/AddWishlist";
    $.ajax({
        url: url,
        data: { id: id },
        success: function (e) {
            if (e._result > 0) {
                $('.addtofav').text("Sản phẩm đã được thêm vào yêu thích");
                $('.addtofav').removeAttr('onclick')
            }
            else {
                alert(e._error);
            }
        }
    })
}