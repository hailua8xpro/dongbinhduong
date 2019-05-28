/// <reference path="jquery-2.0.0.intellisense.js" />
/// <reference path="jquery-2.0.0.min.js" />
$(document).ready(function () {
    $('#btnViewMore').click(function () {
        var pageindexbefore = $('#PageIndex').val();
        var pageindexafter = pageindexbefore + 1;
        $('#PageIndex').val(pageindexafter);
        var arrpropdetailid = [];
        var groupid = $('#GroupId').val();
        var cateid = $('#CateId').val();
        var kw = $('#frmsearchbuildpc input').val();
        $('.property-filter input[type=checkbox]:checked').each(function () {
            var id = $(this).val();
            arrpropdetailid.push(id);
        })
        $('#LstPropDetailId').val(arrpropdetailid.join(','));
        LoadBuildPcProduct(pageindexafter, $('#LstPropDetailId').val(), cateid, groupid, kw);
    })
})
function choseAccessories(id, type) {
    var url = '/aj/product/PopupBuildPC';
    $.ajax({
        url: url,
        data: { type: type, id: id },
        beforeSend: function () {
            showLoading();
        },
        success: function (e) {
            hideLoading();
            if ($('#popupBuildPc').length > 0) {
                $('#popupBuildPc').remove();
            }
            $('body').append(e);
            $('#popupBuildPc').modal();
        },
        error: function () {
            hideLoading();
        }
    })
}
$(document).on('click', '.property-filter input[type=checkbox]', function () {
    var arrpropdetailid = [];
    var groupid = $('#GroupId').val();
    var cateid = $('#CateId').val();
    var kw = $('#frmsearchbuildpc input').val();

    $('.property-filter input[type=checkbox]:checked').each(function () {
        var id = $(this).val();
        arrpropdetailid.push(id);
    })
    $('#LstPropDetailId').val(arrpropdetailid.join(','));
    LoadBuildPcProduct(1, $('#LstPropDetailId').val(), cateid, groupid, kw);
})
$(document).on('submit', '#frmsearchbuildpc', function (e) {
    var arrpropdetailid = [];
    var groupid = $('#GroupId').val();
    var cateid = $('#CateId').val();
    var kw = $('#frmsearchbuildpc input').val();

    $('.property-filter input[type=checkbox]:checked').each(function () {
        var id = $(this).val();
        arrpropdetailid.push(id);
    })
    $('#LstPropDetailId').val(arrpropdetailid.join(','));
    LoadBuildPcProduct(1, $('#LstPropDetailId').val(), cateid, groupid, kw);
    return false;
})
function LoadBuildPcProduct(pageindex, lstpropdetailid, cateId, groupId,kw) {
    var url = '/aj/product/LoadBuildPcProduct';
    $.ajax({
        url: url,
        data: { page: pageindex, lstPropDetailId: lstpropdetailid, cateId: cateId, groupId: groupId,kw:kw },
        beforeSend: function () { showLoading(); },
        success: function (e) {
            hideLoading();
            if (pageindex == 1) {
                $('.buildpclist').html(e._html);
            }
            else {
                $('.buildpclist').append(e._html);
            }
            var totalpage = e._result;
            $('#TotalPage').val(totalpage);
            if (pageindex >= totalpage) {
                $('#btnViewMore').hide();
            }
        }
    })
}

function addToBuild(id, qty, typeid, type) {
    $.ajax({
        url: "/aj/Product/AddToBuild",
        data: { id: id, qty: qty },
        beforeSend: function () { showLoading(); },
        success: function (e) {
            if (e._result > 0) {
                hideLoading();
                $('#popupBuildPc').modal('hide');
                var td = type + '-' + typeid;
                $('.tbl-builPc').find('[data-tdid="' + td + '"]').html(e._html);
            }
            else {
                hideLoading();
                alert(e._error);
            }
        }
    })
}
