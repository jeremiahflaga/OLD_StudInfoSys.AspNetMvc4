
$(function () {

    //alert("asdf");

    if ($('tr:contains(Jboy)').length > 0) {
        $('table').before('<span id="start" style="color: red; font-size: 20px;">Start testing this app with <em><strong>\"Flaga, Jboy\"</strong><em></span>');
        $('tr:contains(Jboy)').css({ color: 'red' });
    }
    
});