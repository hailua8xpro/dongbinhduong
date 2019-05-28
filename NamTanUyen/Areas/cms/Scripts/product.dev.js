/// <reference path="D:\Working\Freelance\NamTanUyen\NamTanUyen\Scripts/jquery-2.0.0.min.js" />


$(document).ready(function () {
 
    $('#btnAddSubCateGroup').click(function () {
        var objid = $('#ProductObjectId').val();
        if (objid < 1 || objid==undefined) {
            alert('Vui lòng chọn đối tượng sản phẩm');
            return false;
        }
        var url = "/cms/ProductCategories/GetListCateByObjectId";
        $.ajax({
            data: { objectId: objid },
            url:url,
            beforeSend: function () { showLoading() },
            success: function (e) {
                hideLoading();
                $('.modal .modal-body').html(e);
                $('#mymodal').modal();
            }
        })
    })
    $('#btnShowCateGroupByPrId').click(function () {
        var objid = $('#ProductObjectId').val();
        if (objid < 1 || objid == undefined) {
            alert('Vui lòng chọn đối tượng sản phẩm');
            return false;
        }
        var url = "/cms/ProductCategories/GetListCateByProductId";
        $.ajax({
            data: { objectId: objid, productId: productid },
            url: url,
            beforeSend: function () { showLoading() },
            success: function (e) {
                hideLoading();
                $('.modal .modal-body').html(e);
                $('#mymodal').modal();
            }
        })
    })
    
})
$(document).on('change', '#ProductCategoryId', function () {
    var id = $(this).val();
    var url = "/cms/ProductGroups/GetGroupDropdownByCateId";
    $.ajax({
        data: { cateId: id },
        url: url,
        beforeSend: function () {
            showLoading();
        },
        success: function (e) {
            hideLoading();
            $('#ProductGroupId').replaceWith(e);
        }
    })
})
$(document).on('click', '.parent-check', function () {
    var checked = $(this).is(':checked');
    if (checked) {
        $(this).closest('li').find('.child-check').prop('checked', true);
    }
    else {
        $(this).closest('li').find('.child-check').prop('checked', false);
    }
})
