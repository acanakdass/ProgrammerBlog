
$(document).ready(function () {
    //Select2 Start
    $('#categoryList').select2({
        theme: 'bootstrap4',
        placeholder: "Bir kategori seciniz",
        allowClear: true
        });

    //Select2 Ends

    //Jquery UI Starts
    $(function () {
        $('#datepicker').datepicker({
            closeText: "kapat",
            prevText: "&#x3C;geri",
            nextText: "ileri&#x3e",
            currentText: "bug�n",
            monthNames: ["Ocak", "�ubat", "Mart", "Nisan", "May�s", "Haziran",
                "Temmuz", "A�ustos", "Eyl�l", "Ekim", "Kas�m", "Aral�k"],
            monthNamesShort: ["Oca", "�ub", "Mar", "Nis", "May", "Haz",
                "Tem", "A�u", "Eyl", "Eki", "Kas", "Ara"],
            dayNames: ["Pazar", "Pazartesi", "Sal�", "�ar�amba", "Per�embe", "Cuma", "Cumartesi"],
            dayNamesShort: ["Pz", "Pt", "Sa", "�a", "Pe", "Cu", "Ct"],
            dayNamesMin: ["Pz", "Pt", "Sa", "�a", "Pe", "Cu", "Ct"],
            weekHeader: "Hf",
            dateFormat: "dd.mm.yy",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: "",
            duration:500,
            showAnim: "fold",
            showOptions: {direction:"up"},
            minDate:-3, //�u anki tarihten 3 g�n �ncesine kdar se�meye izin ver
            maxDate:+3, //�u anki tarihten 3 g�n sonras�na kdar se�meye izin ver
        
        });
    })
    //Jquery UI Ends






    //Trumbowgy Start
    $('#text-editor').trumbowyg({
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize'],

        ]

    });
    //Trumbowgy End
})

