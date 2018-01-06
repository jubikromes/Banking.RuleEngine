
$('#shipadddiv').show();
$('#shipadddiv').hide();


$("#checkBoxLeftParam").change(function () {
    if ($('#chShipAdd').prop('checked')) {
        $('#shipadddiv').show();
    } else {
        $('#shipadddiv').hide();
    }
})