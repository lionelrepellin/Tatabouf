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

function showPopup(result) {
    $('#popup').html(result);
    $('.cache').show();
    $('#popup').show();
};

function ShowError(xhr, ajaxOptions, thrownError) {
    alert(xhr.status + ' - ' + thrownError);
};