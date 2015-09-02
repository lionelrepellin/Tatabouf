function onEditClick(e) {
    e.preventDefault();
    $link = $(this);
    var id = $link.data('id');
    loadDetails(id);
};

function loadDetails(idCrew) {
    $.ajax({
        type: "GET",
        url: "/Home/Edit",
        data: { id: idCrew },
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

function removeItem(idCrew) {
    $.ajax({
        type: "POST",
        url: "/Home/Remove",
        data: { id: idCrew },
        success: function () {
            document.location = "/Home";
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