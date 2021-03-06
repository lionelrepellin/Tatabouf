﻿function onEditClick(e) {
    e.preventDefault();
    $link = $(this);
    var id = $link.data('id');
    loadDetails(id);
};

function loadDetails(foodChoiceId) {
    $.ajax({
        type: "GET",
        url: "/Home/Edit",
        data: { id: foodChoiceId },
        success: showPopup,
        error: ShowError,
        dataType: "html"
    });
};

function showDeletePopup(e) {
    e.preventDefault();
    $link = $(this);
    var id = $link.data('id');
    $('#btn-confirm-yes').data('id-to-remove', id);
    $('#confirmDelete').show();
    $('.cache').show();
};

function onRemoveClick(e) {
    e.preventDefault();
    $link = $(this);
    var id = $link.data('id-to-remove');
    removeItem(id);
};

function removeItem(foodChoiceId) {
    $.ajax({
        type: "POST",
        url: "/Home/Remove",
        data: { id: foodChoiceId },
        success: function () {
            document.location = "/Home/Index";
        },
        error: ShowError
    });
};

function onCancelClick() {
    $('#btn-confirm-yes').data('id-to-remove', '');
    $('#confirmDelete').hide();
    $('.cache').hide();
};

function showPopup(result) {
    $('#popup').html(result);
    $('.cache').show();
    $('#popup').show();
};

function ShowError(xhr, ajaxOptions, thrownError) {
    alert(xhr.status + ' - ' + thrownError);
};

$(function() {
    $('.actions .edit').on('click', onEditClick);
    $('.actions .remove').on('click', showDeletePopup);
    $('#btn-confirm-yes').on('click', onRemoveClick);
    $('#btn-confirm-no').on('click', onCancelClick);
    $('.formulaire .actions .save').on('click', function () {
        $("form").submit();
    });
});