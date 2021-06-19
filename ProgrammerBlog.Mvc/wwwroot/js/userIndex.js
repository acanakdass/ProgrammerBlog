
$(document).ready(() => {
    $('#usersTable').DataTable({
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
                        url: '/Admin/User/GetAllUsers',
                        contentType: "application/Json",
                        beforeSend: () => {
                            //ajax işlemi başlamadan hemen önce çalışacak kod bloğu
                            $('#usersTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const userListDto = jQuery.parseJSON(data);
                            if (userListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(userListDto.Users.$values, function (index, user) {
                                    tableBody += `<tr name="${user.Id}">

                                                            <td>${user.Id}</td>
                                                            <td>${user.FirstName}</td>
                                                            <td>${user.LastName}</td>
                                                            <td>${user.UserName}</td>
                                                            <td>${user.Email}</td>
                                                            <td>${user.PhoneNumber}</td>
                                                            <td>
                                                                <img src="/img/${user.Image}" width="70" alt="${user.UserName}" />
                                                            </td>
                                                            <td style="padding:3px">
                                                                <button id="btnEdit" class="btn btn-primary btn-block btn-sm" data-id="${user.Id}"><i class="fas fa-edit"></i> Düzenle</button>
                                                                <button id="btnDelete" class="btn btn-danger btn-block btn-sm" data-id="${user.Id}"><i class="fas fa-minus-circle"></i> Sil</button>
                                                            </td>
                                                        </tr>`;
                                });
                                $('.spinner-border').hide();

                                //$('#categoriesTable > tbody').replaceWith(tableBody);
                                $("#usersTable > tbody").html(tableBody);
                                $('#usersTable').fadeIn(500);
                            } else {
                                toastr.error('Hata', 'İşlem Başarısız');

                            }
                        },
                        error: (err) => {
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(500);
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
        const url = '/Admin/User/Add';

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
            const form = $('#form-user-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            //serialize() Herhangi bir argüman almayan bir metoddur ve asıl amacı string’e çevrilmiş ve url-encoded edilmiş text üretmektir.
            //actionUrl'e (örn. CategoryController/Add) serialized form verisini post eder
            $.ajax({

                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,

                success: (data) => {   //form post edildi ve isValid ise veritabanına eklendi
                    //data : controller'da post metodunda return edilen değerdir.
                    const userAddAjaxViewModel = jQuery.parseJSON(data); //data ile controllerdan json olarak dönen categoryAddAjaxViewModel json nesnesini
                    //jquery ile okuyabilmek için JSON'dan objeye parse etme.
                    const newFormBody = $('.modal-body', userAddAjaxViewModel.UserAddPartial); //form isvalid :false ise terkar gösterilecek form body'si
                    //verilen CategoryAddPartial içerisindeki modal-body classlı elementi alır
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody); //önceki modal-body alanını yenisiyle değiştirir.
                    console.log(newFormBody.find('[name="IsValid"]').val());
                    const isFormValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isFormValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        $("#btnRefresh").click();
                        Swal.fire(
                            'Başarılı!',
                            `${userAddAjaxViewModel.UserDto.User.UserName} adlı kullanıcı başarıyla oluşturuldu.`,
                            'success'
                        );
                        toastr.success(`${userAddAjaxViewModel.UserDto.Message}`, 'Başarılı İşlem!');
                    } else {
                        $('#validationSummary > ul > li').each(function () {
                            let text = $(this).text();
                            toastr.warning(text);
                        });
                    }
                },
                error: (err) => {
                    console.log(err);
                    toastr.error(err);
                }
            })
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
            title: 'Kullanıcı Sil',
            text: `${deletedRowName} adlı kullanıcıyı silmek istediğinize emin misiniz?`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Kullanıcıyı Sil',
            cancelButtonText: 'Vazgeç'
        }).then((result) => {
            if (result.isConfirmed) {
                console.log("success");
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { userId: id },
                    //url: '@Url.Action("Delete","Category")',
                    url: '/Admin/User/Delete',
                    success: function (data) { //data : IResult gelir
                        const userDto = jQuery.parseJSON(data);
                        console.warn(data);
                        if (userDto.ResultStatus === 0) {
                            console.log("successFiree");
                            Swal.fire(
                                'Başarılı!',
                                `${userDto.User.UserName} adlı kullanıcı başarıyla silindi.`,
                                'success'
                            );
                            toastr.info(`${userDto.User.UserName} adlı kullanıcı başarıyla silindi.`, "Silindi");
                            $('#usersTable').DataTable().row(deletedTableRow).remove().draw();

                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: `${userDto.Message}`,
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
        const url = '/Admin/User/Update';                  //istek yapılacak controller metodu
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '#btnEdit', function (e) {
            e.preventDefault();
            const id = $(this).attr('data-id');                     //seçili tıklanılan butonun 'data-id' attribute değerini alma

            $.get(url, { userId: id }).done(function (data) {            //yukarıda belirtilen url'e get isteği gönder ve yanında categoryId parametresini(controller'ın aldığı parametre) gönder
                placeHolderDiv.html(data);                          //placeholder Div içerisine controllerdan dönen _partialView'i yerleştir
                placeHolderDiv.find('.modal').modal('show');        //placeHolderDiv elementini modal olarak aç
            }).fail(() => {
                toastr.error("İstek gönderilirken bir hata oluştu");
            });
        });

        // Ajax POST/ UPDATE : Updating a category starts from here.

        placeHolderDiv.on('click', '#btnUpdate', function (e) {
            e.preventDefault();
            const form = $('#form-user-update'); //modal içindeki form'a erişme
            const actionUrl = form.attr('action');   //form'un action'ını alır. örn. burdan gelecek değer : /Admin/Category/Add
            const dataToSend = new FormData(form.get(0));//convert form to string(categoryUpdateDto)

            Swal.fire({
                title: 'Kullanıcı Güncelle',
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
                    $.ajax({
                        url: actionUrl,
                        type: 'POST',
                        data: dataToSend,
                        processData: false,
                        contentType: false,
                        success: function (data) {
                            const userUpdateAjaxModel = jQuery.parseJSON(data); //controller'da json'a serialize edip, json tipinde gönderilen userUpdateAjaxModel json nesnesini objeye parse etme
                            console.log("xxxxxxxxxxxxxxxxxxxxxxxxxx");
                            const newFormBody = $('.modal-body', userUpdateAjaxModel.UserUpdatePartial); //categoryUpdateAjaxModel objesi içerisindeki CategoryUpdatePartial
                            //stringi içerisindeki modal-body class'lı elementi seç
                            placeHolderDiv.find('.modal-body').replaceWith(newFormBody); //placeHolderDiv içerisindeki modabody class'lı element ile newFormBody yer değişme
                            const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                            if (isValid) {
                                placeHolderDiv.find('.modal').modal('hide');
                                toastr.success(`${userUpdateAjaxModel.UserDto.Message}`, "Güncelleme İşlemi başarılı");
                                $('#btnRefresh').click();
                                Swal.fire(
                                    'Başarılı!',
                                    `${userUpdateAjaxModel.UserDto.User.UserName} adlı kullanıcı başarıyla güncellendi.`,
                                    'success'
                                );
                            }
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                }
            });



        })

    });
});