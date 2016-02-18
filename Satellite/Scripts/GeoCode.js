function clearResult() {
    setResult("");
}

function setResult(value) {
    $('#searchResultDiv').html(value);
}

$('#btnSubmit').click(function () {
    $("#btnSubmit").attr("disabled", "disabled").text("Searching...");
    clearResult();
    $('#formSearchGeoCode').submit();
});