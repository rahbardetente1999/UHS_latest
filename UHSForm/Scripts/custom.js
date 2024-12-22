// next prev
var divs = $('.show-section section');
var now = 0; // currently shown div
divs.hide().first().show(); // hide all divs except first

function next() {
    divs.eq(now).hide();
    now = (now + 1 < divs.length) ? now + 1 : 0;
    divs.eq(now).show(); // show next
}

$(".prev").click(function () {
    divs.eq(now).hide();
    now = (now > 0) ? now - 1 : divs.length - 1;
    divs.eq(now).show(); // show previous
});


// focus input
//$(document).ready(function () {
//    $(".text_input input[type=text]").focus(function () {
//        $(this).closest(".text_input").addClass('focused');
//    })
//        .blur(function () {
//            $(this).closest(".text_input").removeClass('focused');
//        })
//})

$(document).ready(function () {
    // Listen for change event on the radio button
    //$('#newCustomerRadio').change(function () {
    //    if ($(this).is(':checked')) { // Check if the radio button is checked
    //        next(); // Call the next() function
    //    }
    //});
   
});





// Event handler for radio buttons to remove invalid class on click
$("input[type='radio']").on('click', function () {
    var radioGroupName = $(this).attr('name');
    $("input[name='" + radioGroupName + "']").removeClass('invalid');
});

$(document).ready(function () {
    // Remove 'invalid' class when a radio button is selected
    $("input[name='customertype']").on('change', function () {
        $('#customertype').removeClass('invalid');
    });

  

    // Add click event for step 1 button
    $("#step1btn").on('click', function () {
        if (!$("input[name='customertype']:checked").val()) {
            $('#customertype').addClass('invalid');
            return false;
        }
        else {
            $('#customertype').removeClass('invalid'); // Remove 'invalid' class from customertype
            next();
        }
    });
   
    $("#step2btn").on('click', function () {
        /* next();*/
        
        if (!$("input[name='servicetype']:checked").val()) {
            $('#servicetypeID').addClass('invalid');
            return false;
        }

        else if ($("input[name='servicetype']:checked").val() === "Residential") {
            $('#servicetypeID').removeClass('invalid');
           
            if (!$("input[name='subcategory']:checked").val()) {
                $('#subcategoryID').addClass('invalid');
                return false;
            }
            
            else if ($("input[name='subcategory']:checked").val() === "Regular Cleaning") {
                $('#subcategoryID').removeClass('invalid');
                next();

            }
            else if ($("input[name='subcategory']:checked").val() === "Deep Cleaning") {
                $('#subcategoryID').removeClass('invalid');
                next();
            }
            else if ($("input[name='subcategory']:checked").val() === "Specialized Cleaning") {
                $('#subcategoryID').removeClass('invalid');
                if (!$("input[name='subservice']:checked").val()) {
                    $('#subserviceID').addClass('invalid');
                    return false;
                }
                else if ($("input[name='subservice']:checked").val() == "Sofa Cleaning") {
                    $('#subserviceID').removeClass('invalid');
                    if (!$("input[name='serviceoptiontype']:checked").val()) {
                        $('#serviceoptiontypeID').addClass('invalid');
                        return false;
                    } else {
                        var isValid = true;

                        $("input[name='serviceoptiontype']:checked").each(function () {
                            var quantityInput = $(this).closest('.checkbox-card').find('.form-control');
                           
                            var quantity = quantityInput.val();

                            if (quantity === "" || quantity <= 0) {
                                quantityInput.addClass('invalid');
                                isValid = false;
                            } else {
                                quantityInput.removeClass('invalid');
                                next();
                            }
                        });

                        if (!isValid) {
                            return false;
                        }
                    }
                  
                }
                else if ($("input[name='subservice']:checked").val() == "Mattress Cleaning") {
                    $('#subserviceID').removeClass('invalid');
                    if (!$("input[name='serviceoptiontype']:checked").val()) {
                        $('#serviceoptiontypeID').addClass('invalid');
                        return false;
                    } else {
                        var isValid = true;

                        $("input[name='serviceoptiontype']:checked").each(function () {
                            var quantityInput = $(this).closest('.checkbox-card').find('.form-control');

                            var quantity = quantityInput.val();

                            if (quantity === "" || quantity <= 0) {
                                quantityInput.addClass('invalid');
                                isValid = false;
                            } else {
                                quantityInput.removeClass('invalid');
                                next();
                            }
                        });

                        if (!isValid) {
                            return false;
                        }
                    }

                }

                else if ($("input[name='subservice']:checked").val() == "Carpet Shampooing") {
                    $('#subserviceID').removeClass('invalid');
                    if (!$("input[name='serviceoptiontype']:checked").val()) {
                        $('#serviceoptiontypeID').addClass('invalid');
                        return false;
                    } else {
                        var isValid = true;

                        $("input[name='serviceoptiontype']:checked").each(function () {
                            var quantityInput = $(this).closest('.checkbox-card').find('.form-control');

                            var quantity = quantityInput.val();

                            if (quantity === "" || quantity <= 0) {
                                quantityInput.addClass('invalid');
                                isValid = false;
                            } else {
                                quantityInput.removeClass('invalid');
                                next();
                            }
                        });

                        if (!isValid) {
                            return false;
                        }
                    }

                }
            }
        }

        else if ($("input[name='servicetype']:checked").val() === "Car Wash") {
            $('#CustomerID').remove('invalid');
            return false;
        }

        else {
            $('#servicetypeID').removeClass('invalid');
        }
    });
    $("#step3btn").on('click', function () {
        next();
    });
    $("#step4btn").on('click', function () {
        next();
    });
    $("#step5btn").on('click', function () {
        next();
    });
    $("#step6btn").on('click', function () {
        next();
    });

    // Add click event listener for other step buttons (step3btn, step4btn, step5btn) similarly
    // ...

    // Function to handle submission
    $("#sub").on('click', function () {
        // Final form submission logic
    });
});













