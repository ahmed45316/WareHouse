

$(document).ready(function () {
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#productCategory').val() == "0") {
            isAllValid = false;
            $('#productCategory').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#productCategory').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#product').val() == "0") {
            isAllValid = false;
            $('#product').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#product').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }
        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.pc', $newRow).val($('#productCategory').val());
            $('.product', $newRow).val($('#product').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#productCategory,#product,#quantity,#price,#totalprice,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#productCategory,#product').val('0');
            $('#quantity,#price,#totalprice').val('');
            $('#orderItemError').empty();
        }

    })
    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });
    //submit
    $('#form-submit').click(function (e) {
        e.preventDefault();
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tr').each(function (index, ele) {
            if (
                $('select.product', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 ||
                $('.price', this).val() == "" ||
                isNaN($('.price', this).val())
            ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    itemId: $('select.product', this).val(),
                    quantity: parseInt($('.quantity', this).val()),
                    price: parseFloat($('.price', this).val()),
                    totalPrice: parseFloat($('.totalprice', this).val()),
                    invoicId: 0
                }
                list.push(orderItem);
            }
        })


        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least 1 order item required.');
            isAllValid = false;
        }



        if (isAllValid) {
            var data = {
                customerId: $('#CustomerId').val().trim(),
                invoicType: parseInt($('#InvoicType').val().trim()),
                invoiceDateTime: new Date(), //$('#InvoiceDateTime').val()
                invoiceNumber: $('#InvoiceNumber').val().trim(),
                invoiceTotal: $('#InvoiceTotal').val().trim(),
                invoiceDetails: list,
                isContinue: true
            }

            $(this).val('Please wait...');

            if ($("#formData").valid()) {
                $.ajax({
                    type: 'POST',
                    url: `${baseUrl}Invoice/Add`,
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    success: function (data) {
                        toastr.success('Successfully saved');
                        window.location.href = index;
                        //if (data.status) {
                            //alert('Successfully saved');
                            ////here we will clear the form
                            //list = [];
                            //$('#orderNo,#orderDate,#description').val('');
                            //$('#orderdetailsItems').empty();
                        //}
                        //else {
                            //alert('Error');
                        //}
                        $('#submit').val('Save');
                    },
                    error: function (error) {
                        console.log(error);
                        $('#submit').val('Save');
                    }
                });
            }
        }
    });
});

function LoadProduct(event) {
    //console.log('this event', $(event).val());
    $.ajax({
        type: "GET",
        url: `${baseUrl}Item/GetByCategoryId/${$(event).val()}`,
        //data: { 'categoryID': $(categoryDD).val() },
        success: function (data) {
            //render products to appropriate dropdown
            // renderProduct($(event).parents('.mycontainer').find('select.product'), data);
            renderProduct('#product', data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderProduct(element, data) {
    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.itemName));
    })
}
function CheckQuantity(event) {
    var currentQty = $(event).val();
    $.ajax({
        type: "GET",
        url: `${baseUrl}Invoice/GetItemStock/${$('#product').val()}`,
        //data: { 'categoryID': $(categoryDD).val() },
        success: function (data) {
            //render products to appropriate dropdown
            // renderProduct($(event).parents('.mycontainer').find('select.product'), data);
            let value = data.quantity <= currentQty ? data.quantity : currentQty;
            console.log('val', value);
            $('#quantity').val(value);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

