function onEditClick(e) {
    e.preventDefault();
    $link = $(this);
    var id = $link.data('id');
    LoadDetails(id);
};

function LoadDetails(idCrew) {
    $.ajax({
        type: "GET",
        url: "/Home/Edit",
        data: { id: idCrew },
        success: ShowPopup,
        error: ShowError,
        dataType: "html"
    });
};

function ShowPopup(result) {
    $('#popup').html(result);
    $('#popup').show();
};

function ShowError(xhr, ajaxOptions, thrownError) {
    alert(xhr.status);
    alert(thrownError);
};

function reload() {
    console.log('reload the page !');
};

function omg() {
    console.log('tout est buggué !');
};