
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
            currentText: "bugün",
            monthNames: ["Ocak", "Þubat", "Mart", "Nisan", "Mayýs", "Haziran",
                "Temmuz", "Aðustos", "Eylül", "Ekim", "Kasým", "Aralýk"],
            monthNamesShort: ["Oca", "Þub", "Mar", "Nis", "May", "Haz",
                "Tem", "Aðu", "Eyl", "Eki", "Kas", "Ara"],
            dayNames: ["Pazar", "Pazartesi", "Salý", "Çarþamba", "Perþembe", "Cuma", "Cumartesi"],
            dayNamesShort: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
            dayNamesMin: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
            weekHeader: "Hf",
            dateFormat: "dd.mm.yy",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: "",
            duration:500,
            showAnim: "fold",
            showOptions: {direction:"up"},
            minDate:-3, //Þu anki tarihten 3 gün öncesine kdar seçmeye izin ver
            maxDate:+3, //Þu anki tarihten 3 gün sonrasýna kdar seçmeye izin ver
        
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

