$(document).ready(() => {
    $('#categoriesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd"
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                attr: {
                    id: "btnRefresh"
                },
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        //url: '@Url.Action("GetNonDeletedCategories","Category")',
                        url: '/Admin/Category/GetNonDeletedCategories',
                        contentType: "application/Json",
                        beforeSend: () => {
                            //ajax işlemi başlamadan hemen önce çalışacak kod bloğu
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            if (categoryListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(categoryListDto.Categories.$values, function (index, category) {
                                    tableBody += `<tr name="${category.Id}">

                                                            <td>${category.Id}</td>
                                                            <td>${category.Name}</td>
                                                            <td>${category.Description}</td>
                                                            <td>${category.IsActive.toString()}</td>
                                                            <td>${category.IsDeleted.toString()}</td>
                                                            <td>${convertToShorterDate(category.CreatedDate)}</td>
                                                            <td>${category.CreaterName}</td>
                                                            <td>${category.ModifiedDate}</td>
                                                            <td>${category.ModifierName}</td>
                                                            <td style="padding:3px">
                                                                <button id="btnEdit" class="btn btn-primary btn-block btn-sm" data-id="${category.Id}"><i class="fas fa-edit"></i> Düzenle</button>
                                                                <button id="btnDelete" class="btn btn-danger btn-block btn-sm" data-id="${category.Id}"><i class="fas fa-minus-circle"></i> Sil</button>
                                                            </td>
                                                        </tr>`;
                                });
                                $('.spinner-border').hide();

                                //$('#categoriesTable > tbody').replaceWith(tableBody);
                                $("#categoriesTable > tbody").html(tableBody);
                                $('#categoriesTable').fadeIn(500);
                            } else {
                                toastr.error('Hata', 'İşlem Başarısız');

                            }
                        },
                        error: (err) => {
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(500);
                            toastr.error(`$(err.responseText)`, 'İşlem Başarısız');

                        }
                    })
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi",
                    "0": "-"
                },
                "0": "-",
                "1": "%d satır seçildi",
                "2": "-",
                "_": "%d satır seçildi",
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "info": "-",
                "fill": "Bütün hücreleri <i>%d<\/i> ile doldur"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "1": "-",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır",
                "copy": "Kopyala",
                "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın."
            },
            "infoPostFix": "-",
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar"
                    },
                    "array": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "without": "Hariç"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı çıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer",
                "clearAll": "Filtreleri Temizle"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d"
            },
            "searchPlaceholder": "Ara",
            "thousands": ".",
            "datetime": {
                "amPm": [
                    "öö",
                    "ös"
                ],
                "hours": "Saat",
                "minutes": "Dakika",
                "next": "Sonraki",
                "previous": "Önceki",
                "seconds": "Saniye",
                "unknown": "Bilinmeyen"
            },
            "decimal": ",",
            "editor": {
                "close": "Kapat",
                "create": {
                    "button": "Yeni",
                    "submit": "Kaydet",
                    "title": "Yeni kayıt oluştur"
                },
                "edit": {
                    "button": "Düzenle",
                    "submit": "Güncelle",
                    "title": "Kaydı düzenle"
                },
                "error": {
                    "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
                },
                "multi": {
                    "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                    "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                    "restore": "Değişiklikleri geri al",
                    "title": "Çoklu değer"
                },
                "remove": {
                    "button": "Sil",
                    "confirm": {
                        "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                        "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                    },
                    "submit": "Sil",
                    "title": "Kayıtları sil"
                }
            }
        }
    });
    //Datatable ends here


    $(function () {
        // Ajax Get : Getting the _CategoryAddPartial as Modal Form starts from here.

        //const url = '@Url.Action("Add","Category")';
        const url = '/Admin/Category/Add';

        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(() => {
            //get işlemi CategoryController/Add isimli action'a gidip partialView'i getirir.
            $.get(url).done((data) => { //_CategoryAddPartial view'ini data olarak döndürür.
                placeHolderDiv.html(data);
                //partialView'i html olarak placeholderDiv elementinin içine ekler.
                placeHolderDiv.find(".modal").modal('show'); //find ile "modal" class'ı içeren div'i bulur ve modal olarak açar
            });
        });
        // Ajax GET : Getting the _CategoryAddPartial as Modal Form ENDS here.




        // Ajax POST : Posting the form data as CategoryAddDto   starts from here.
        placeHolderDiv.on('click', '#btnSave', (e) => {
            e.preventDefault(); //submit olayı ile sayfanın yenilenmesini engelle
            const form = $('#form-category-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();  //form'dan gelen veriyi categoryAddDto'ya çevir
            //serialize() Herhangi bir argüman almayan bir metoddur ve asıl amacı string’e çevrilmiş ve url-encoded edilmiş text üretmektir.
            //actionUrl'e (örn. CategoryController/Add) serialized form verisini post edet
            $.post(actionUrl, dataToSend).done((data) => {   //form post edildi ve isValid ise veritabanına eklendi
                //data : controller'da post metodunda return edilen değerdir.
                const categoryAddAjaxViewModel = jQuery.parseJSON(data); //data ile controllerdan json olarak dönen categoryAddAjaxViewModel json nesnesini
                //jquery ile okuyabilmek için JSON'dan objeye parse etme.
                const newFormBody = $('.modal-body', categoryAddAjaxViewModel.CategoryAddPartial); //form isvalid :false ise terkar gösterilecek form body'si
                //verilen CategoryAddPartial içerisindeki modal-body classlı elementi alır
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody); //önceki modal-body alanını yenisiyle değiştirir.
                console.log(newFormBody.find('[name="IsValid"]').val());
                const isFormValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isFormValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `<tr name="${categoryAddAjaxViewModel.CategoryDto.Category.Id}">
                                <td>${categoryAddAjaxViewModel.CategoryDto.Category.Id}</td>
                                <td>${categoryAddAjaxViewModel.CategoryDto.Category.Name}</td>
                                <td>${categoryAddAjaxViewModel.CategoryDto.Category.Description}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxViewModel.CategoryDto.Category.IsActive.toString())}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxViewModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                <td>${convertToShorterDate(categoryAddAjaxViewModel.CategoryDto.Category.CreatedDate)}</td>
                                <td>${categoryAddAjaxViewModel.CategoryDto.Category.CreaterName}</td>
                                <td>${categoryAddAjaxViewModel.CategoryDto.Category.ModifiedDate}</td>
                                <td>${categoryAddAjaxViewModel.CategoryDto.Category.ModifierName}</td>
                                <td style="padding:3px">
                                        <button id="btnEdit" class="btn btn-primary btn-block btn-sm"><i class="fas fa-edit" data-id="${categoryAddAjaxViewModel.CategoryDto.Category.Id}"></i> Düzenle</button>
                                        <button id="btnDelete" class="btn btn-danger btn-block btn-sm" data-id="${categoryAddAjaxViewModel.CategoryDto.Category.Id}"><i class="fas fa-minus-circle"></i> Sil</button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow); // string'i bir objeye çevirdik
                    $('#categoriesTable').append(newTableRowObject);
                    newTableRowObject.hide();
                    //newTableRowObject.fadeIn(500);
                    newTableRowObject.effect("highlight", { color: "#a5dc86" }, 2000);
                    Swal.fire(
                        'Başarılı!',
                        `${categoryAddAjaxViewModel.CategoryDto.Category.Name} adlı kategori başarıyla oluşturuldu.`,
                        'success'
                    );
                    toastr.success(`${categoryAddAjaxViewModel.CategoryDto.Message}`, 'Başarılı İşlem!');
                } else {
                    $('#validationSummary > ul > li').each(function () {
                        let text = $(this).text();
                        toastr.warning(text);
                    });
                }
            });
        });
    });
    // Ajax POST : Posting the form data as CategoryAddDto ENDS here.





    // Ajax POST : Deleting a category starts from here.
    $(document).on('click', '#btnDelete', function () {
        //sweetAlert modal
        const id = $(this).attr('data-id');
        const deletedTableRow = $(`[name="${id}"]`);
        const deletedRowName = deletedTableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Kategori Sil',
            text: `${deletedRowName} adlı kategoriyi silmek istediğinize emin misiniz?`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Kategoriyi Sil',
            cancelButtonText: 'Vazgeç'
        }).then((result) => {
            if (result.isConfirmed) {
                console.log("success");
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { categoryId: id },
                    //url: '@Url.Action("Delete","Category")',
                    url: '/Admin/Category/Delete',
                    success: function (data) { //data : IResult gelir
                        const categoryDto = jQuery.parseJSON(data);
                        console.log(data);
                        if (categoryDto.ResultStatus === 0) {
                            console.log("successFiree");
                            Swal.fire(
                                'Başarılı!',
                                `${categoryDto.Category.Name} adlı kategori başarıyla silindi.`,
                                'success'
                            );
                            toastr.info(`${categoryDto.Category.Name} adlı kategori başarıyla silindi.`, "Silindi");
                            deletedTableRow.fadeOut(1000);

                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: `${categoryDto.message}`,
                            });
                        }
                    },
                    error: (err) => {
                        toastr.warning("Hata", err);
                    },
                });

            }
        });
    });




    $(function () {
        const url = '/Admin/Category/Update';                  //istek yapılacak controller metodu
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '#btnEdit', function (e) {
            e.preventDefault();
            const id = $(this).attr('data-id');                     //seçili tıklanılan butonun 'data-id' attribute değerini alma
            $.get(url, { categoryId: id }).done(function (data) {            //yukarıda belirtilen url'e get isteği gönder ve yanında categoryId parametresini(controller'ın aldığı parametre) gönder
                placeHolderDiv.html(data);                          //placeholder Div içerisine controllerdan dönen _partialView'i yerleştir
                placeHolderDiv.find('.modal').modal('show');        //placeHolderDiv elementini modal olarak aç
            }).fail(() => {
                toastr.error("İstek gönderilirken bir hata oluştu");
            });
        });

        // Ajax POST/ UPDATE : Updating a category starts from here.

        placeHolderDiv.on('click', '#btnUpdate', function (e) {
            e.preventDefault();
            const form = $('#form-category-update'); //modal içindeki form'a erişme
            const actionUrl = form.attr('action');   //form'un action'ını alır. örn. burdan gelecek değer : /Admin/Category/Add
            const dataToSend = form.serialize(); //convert form to string(categoryUpdateDto)

            Swal.fire({
                title: 'Kategori Güncelle',
                text: `Değişiklikleri kaydetmek istediğinize emin misiniz?`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Değişiklikleri Kaydet',
                cancelButtonText: 'Vazgeç'
            }).then((result) => {
                if (result.isConfirmed) {
                    console.log("success");
                    


                    $.post(actionUrl, dataToSend).done((data) => {
                        const categoryUpdateAjaxModel = jQuery.parseJSON(data); //controller'da json'a serialize edip, json tipinde gönderilen categoryUpdateAjaxModel json nesnesini objeye parse etme
                        console.log("xxxxxxxxxxxxxxxxxxxxxxxxxx");
                        const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial); //categoryUpdateAjaxModel objesi içerisindeki CategoryUpdatePartial
                        //stringi içerisindeki modal-body class'lı elementi seç
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody); //placeHolderDiv içerisindeki modabody class'lı element ile newFormBody yer değişme
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True'
                        //console.log(isValid);
                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');

                                Swal.fire(
                                    'Başarılı!',
                                    `${categoryUpdateAjaxModel.CategoryDto.Message}`,
                                    'success'
                                );
                            toastr.info(`${categoryUpdateAjaxModel.CategoryDto.Message}`, "Güncelleme İşlemi başarılı");
                            $('#btnRefresh').click();


                        }
                    }).fail((res) => {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Bir hata oluştu:(',
                        });
                        toastr.error(res);
                    });



                }
            });

            

        })

    });
});