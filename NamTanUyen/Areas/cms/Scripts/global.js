function addModel(url, data, formname) {
    if (!flg) {
        return false;
    }
    flg = false;
    $.ajax({
        method: 'POST',
        url: url,
        data: data,
        beforeSend: function () {
            showLoading();
        },
        success: function (e) {
            flg = true;
            hideLoading();
            if (e._result === 1) {

                alert("Thêm mới thành công");
                $(formname).trigger('reset');
                if (typeof CKEDITOR !== "undefined" && CKEDITOR != null) {
                    CKupdate();
                }
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
    function addModel(url, data, formname) {
        if (!flg) {
            return false;
        }
        flg = false;
        $.ajax({
            method: 'POST',
            url: url,
            data: data,
            beforeSend: function () {
                showLoading();
            },
            success: function (e) {
                flg = true;
                hideLoading();
                if (e._result === 1) {
                    alert("Thêm mới thành công");
                    $(formname).trigger('reset');
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
    }
}
function updateModel(url, data) {
    if (!flg) {
        return false;
    }
    flg = false;
    $.ajax({
        method: 'POST',
        url: url,
        data: data,
        beforeSend: function () {
            showLoading();
        },
        success: function (e) {
            flg = true;
            hideLoading();
            if (e._result === 1) {

                alert("Cập nhật thành công");
                location.reload();
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
    function addModel(url, data, formname) {
        if (!flg) {
            return false;
        }
        flg = false;
        $.ajax({
            method: 'POST',
            url: url,
            data: data,
            beforeSend: function () {
                showLoading();
            },
            success: function (e) {
                flg = true;
                hideLoading();
                if (e._result === 1) {

                    alert("Thêm mới thành công");
                    $(formname).trigger('reset');
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
    }
}

function addModelWithImage(url, data, formname) {
    if (!flg) {
        flg = true;
        return false;
    }
    flg = false;
    $.ajax({
        method: 'POST',
        url: url,
        data: data,
        processData: false,
        contentType: false,
        beforeSend: function () {
            showLoading();
        },
        success: function (e) {
            flg = true;
            hideLoading();
            if (e._result === 1) {
                alert("Thêm mới thành công");
                if (typeof CKEDITOR !== "undefined" && CKEDITOR != null) {
                    CKupdate();
                }
                $(formname).trigger('reset');
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
}

function updateModelWithImage(url, data) {
    if (!flg) {
        flg = true;
        return false;
    }
    flg = false;
    $.ajax({
        method: 'POST',
        url: url,
        data: data,
        processData: false,
        contentType: false,
        beforeSend: function () {
            showLoading();
        },
        success: function (e) {
            flg = true;
            hideLoading();
            if (e._result === 1) {

                alert("Cập nhật thành công");
                location.reload();
            }
            else {
                alert(e._error);
                flg = true;
            }
        },
        error: function (e) {
            flg = true;
            hideLoading();
        }
    });
}
function CKupdate() {
    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }
    CKEDITOR.instances[instance].setData('');
}
function showLoading() {
    $('#dlding').fadeIn();
}
function hideLoading() {
    $('#dlding').fadeOut();
}