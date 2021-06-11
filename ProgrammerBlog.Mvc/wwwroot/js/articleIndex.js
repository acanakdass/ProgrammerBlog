

$('#articlesTable').DataTable({
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
                //$.ajax({
                //    type: 'GET',
                //    //url: '@Url.Action("GetNonDeletedCategories","Category")',
                //    url: '/Admin/Category/GetNonDeletedCategories',
                //    contentType: "application/Json",
                //    beforeSend: () => {
                //        //ajax işlemi başlamadan hemen önce çalışacak kod bloğu
                //        $('#categoriesTable').hide();
                //        $('.spinner-border').show();
                //    },
                //    success: function (data) {
                //        const categoryListDto = jQuery.parseJSON(data);
                //        if (categoryListDto.ResultStatus === 0) {
                //            let tableBody = "";
                //            $.each(categoryListDto.Categories.$values, function (index, category) {
                //                tableBody += `<tr name="${category.Id}">

                //                                            <td>${category.Id}</td>
                //                                            <td>${category.Name}</td>
                //                                            <td>${category.Description}</td>
                //                                            <td>${category.IsActive.toString()}</td>
                //                                            <td>${category.IsDeleted.toString()}</td>
                //                                            <td>${convertToShorterDate(category.CreatedDate)}</td>
                //                                            <td>${category.CreaterName}</td>
                //                                            <td>${category.ModifiedDate}</td>
                //                                            <td>${category.ModifierName}</td>
                //                                            <td style="padding:3px">
                //                                                <button id="btnEdit" class="btn btn-primary btn-block btn-sm" data-id="${category.Id}"><i class="fas fa-edit"></i> Düzenle</button>
                //                                                <button id="btnDelete" class="btn btn-danger btn-block btn-sm" data-id="${category.Id}"><i class="fas fa-minus-circle"></i> Sil</button>
                //                                            </td>
                //                                        </tr>`;
                //            });
                //            $('.spinner-border').hide();

                //            //$('#categoriesTable > tbody').replaceWith(tableBody);
                //            $("#categoriesTable > tbody").html(tableBody);
                //            $('#categoriesTable').fadeIn(500);
                //        } else {
                //            toastr.error('Hata', 'İşlem Başarısız');

                //        }
                //    },
                //    error: (err) => {
                //        $('.spinner-border').hide();
                //        $('#categoriesTable').fadeIn(500);
                //        toastr.error(`$(err.responseText)`, 'İşlem Başarısız');

                //    }
                //})
            }
        }
    ]
});