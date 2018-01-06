$('#leftOperatorParamId').attr('disabled', true);
$('#rightOperatorParamId').attr('disabled', true);



$("#leftcheckBoxAdd").change(function () {
    if ($('#leftcheckBoxAdd').prop('checked')) {
        $('#leftOperatorParamId').attr('disabled', false);
        $('#leftOperatorId').attr('disabled', true);
    } else {
        $('#leftOperatorParamId').attr('disabled', true);
        $('#leftOperatorId').attr('disabled', false);
    }
})


$("#rightcheckBoxAdd").change(function () {
    if ($('#rightcheckBoxAdd').prop('checked')) {
        $('#rightOperatorParamId').attr('disabled', false);
        $('#rightOperatorId').attr('disabled', true);
    } else {
        $('#rightOperatorParamId').attr('disabled', true);
        $('#rightOperatorId').attr('disabled', false);
    }
})

