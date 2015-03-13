/********************************************* $.submitOnEnterKey *********************************************/
$.submitOnEnterKey = function (txtId, targetId) {
    $('#' + txtId).keypress(function (event) {
        if (event.keyCode == 13) {
            $("#" + targetId).focus().click();
            var script = '';
            if ($('#' + targetId).prop('nodeName') == 'A') {
                href = $('#' + targetId).attr('href');
                if (href.indexOf('javascript:') == 0) script = href.substring(11);
            }
            setTimeout("eval('" + script.replace(/'/g, "\\'") + "'); ", 10);
        }
    });
};

String.prototype.TrimEnd = function (v)
{
    if (this.substring(this.length - 1, this.length) == v) {
        return this.substring(0, this.length - 1);
    }
    else {
        return this;
    }
}