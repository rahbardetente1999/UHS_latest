"use strict";
var KTCreateAccount = (function () {
    var e, t, i, o, a, r, s = [];

    function attachRadioListeners() {
        console.log("Attaching radio button event listeners");

        var frequencyRadios = i.querySelectorAll('[name="frequency"]');
        console.log(frequencyRadios);  // Log all frequency radio buttons
        frequencyRadios.forEach(function (radio) {
            radio.addEventListener("change", function () {
                console.log("Frequency radio button changed");
                s[1].revalidateField("frequency");
            });
        });

        var dayslotRadios = i.querySelectorAll('[name="dayslot"]');
        console.log(dayslotRadios);  // Log all dayslot radio buttons
        dayslotRadios.forEach(function (radio) {
            radio.addEventListener("change", function () {
                console.log("Dayslot radio button changed");
                s[1].revalidateField("dayslot");
            });
        });
    }

    return {
        init: function () {
            (e = document.querySelector("#kt_modal_create_account")) && new bootstrap.Modal(e),
                (t = document.querySelector("#kt_create_account_stepper")) &&
                ((i = t.querySelector("#kt_create_account_form")),
                    (o = t.querySelector('[data-kt-stepper-action="submit"]')),
                    (a = t.querySelector('[data-kt-stepper-action="next"]')),
                    (r = new KTStepper(t)).on("kt.stepper.changed", function (e) {
                        4 === r.getCurrentStepIndex()
                            ? (o.classList.remove("d-none"), o.classList.add("d-inline-block"), a.classList.add("d-none"))
                            : 5 === r.getCurrentStepIndex()
                                ? (o.classList.add("d-none"), a.classList.add("d-none"))
                                : (o.classList.remove("d-inline-block"), o.classList.remove("d-none"), a.classList.remove("d-none"));
                    }),
                    r.on("kt.stepper.next", function (e) {
                        console.log("stepper.next");
                        var t = s[r.getCurrentStepIndex() - 1];
                        t
                            ? t.validate().then(function (t) {
                                console.log("validated!");
                                if ("Valid" == t) {
                                    e.goNext();
                                    KTUtil.scrollTop();
                                } else {
                                    Swal.fire({
                                        text: "Sorry, looks like there are some errors detected, please try again.",
                                        icon: "error",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: { confirmButton: "btn btn-light" },
                                    }).then(function () {
                                        KTUtil.scrollTop();
                                    });
                                }
                            })
                            : (e.goNext(), KTUtil.scrollTop());
                    }),
                    r.on("kt.stepper.previous", function (e) {
                        console.log("stepper.previous"), e.goPrevious(), KTUtil.scrollTop();
                    }),
                    s.push(
                        FormValidation.formValidation(i, {
                            fields: {
                                MainCategory: {
                                    validators: {
                                        notEmpty: {
                                            message: "Main Category is required"
                                        }
                                    }
                                },
                                SubCategory: {
                                    validators: {
                                        notEmpty: {
                                            message: "Sub Category is required"
                                        }
                                    }
                                },
                                ServiceCategory: {},
                                serviceoptiontype: {
                                    validators: {
                                        callback: {
                                            message: "At least one service option and its quantity are required if a service category is selected",
                                            callback: function (input) {
                                                var subCategoryField = i.querySelector('[name="SubCategory"]');
                                                var subCategoryObj = JSON.parse(subCategoryField.value);
                                                var nextServices = subCategoryObj.NextServices;

                                                if (nextServices === 0) {
                                                    return true; // Skip validation if NextServices is 0
                                                }

                                                var serviceCategoryField = i.querySelector('[name="ServiceCategory"]');
                                                if (!serviceCategoryField.value) {
                                                    return {
                                                        valid: false,
                                                        message: "Service Category is required"
                                                    };
                                                }

                                                var checkboxes = document.querySelectorAll('input[name="serviceoptiontype"]');
                                                var isAnyChecked = false;
                                                var isValid = true;

                                                checkboxes.forEach(function (checkbox) {
                                                    if (checkbox.checked) {
                                                        isAnyChecked = true;
                                                        var quantityInput = checkbox.closest('.checkbox-card').querySelector('input[type="number"]');
                                                        if (!quantityInput || quantityInput.value <= 0) {
                                                            isValid = false;
                                                        }
                                                    }
                                                });

                                                if (!isAnyChecked) {
                                                    return {
                                                        valid: false,
                                                        message: "field is required"
                                                    };
                                                }

                                                if (!isValid) {
                                                    return {
                                                        valid: false,
                                                        message: "quantity is required"
                                                    };
                                                }

                                                return true;
                                            }
                                        }
                                    }
                                }
                            },
                            plugins: {
                                trigger: new FormValidation.plugins.Trigger(),
                                bootstrap: new FormValidation.plugins.Bootstrap5({
                                    rowSelector: ".fv-row",
                                    eleInvalidClass: "",
                                    eleValidClass: ""
                                })
                            }
                        })
                    ),
                    s.push(
                        FormValidation.formValidation(i, {
                            fields: {
                                Area: { validators: { notEmpty: { message: "Area is required" } } },
                                Property: { validators: { notEmpty: { message: "Property is required" } } },
                                ResidentType: { validators: { notEmpty: { message: "ResidentType is required" } } },
                                ServTType: {
                                    validators: {
                                        callback: {
                                            callback: function () {
                                                var selectedValue = $('[name="ServTType"]:checked').val();
                                                console.log(selectedValue);
                                                if (selectedValue === "Yes") {
                                                    return true; // Skip validation if NextServices is 0
                                                }
                                                else if (servTTypeValue === "No") {
                                                    // If ServTType is No, then validate ConfirmKey
                                                    var confirmKeyValue = $('[name="ConfirmKey"]:checked').val(); // Get the value of ConfirmKey
                                                    console.log(confirmKeyValue); // For debugging
                                                  
                                                }
                                            }
                                        }

                                    }
                                },
                              
                            },
                            plugins: {
                                trigger: new FormValidation.plugins.Trigger(),
                                bootstrap: new FormValidation.plugins.Bootstrap5({
                                    rowSelector: ".fv-row",
                                    eleInvalidClass: "",
                                    eleValidClass: ""
                                })
                            },
                        })
                    ),
                    o.addEventListener("click", function (e) {
                        s[0].validate().then(function (t) {
                            console.log("validated!");
                            if ("Valid" == t) {
                                e.preventDefault();
                                o.disabled = !0;
                                o.setAttribute("data-kt-indicator", "on");
                                setTimeout(function () {
                                    o.removeAttribute("data-kt-indicator");
                                    o.disabled = !1;
                                    r.goNext();
                                }, 2000);
                            } else {
                                Swal.fire({
                                    text: "Sorry, looks like there are some errors detected, please try again.",
                                    icon: "error",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: { confirmButton: "btn btn-light" },
                                }).then(function () {
                                    KTUtil.scrollTop();
                                });
                            }
                        });
                    }),
                    $(i.querySelector('[name="MainCategory"]')).on("change", function () {

                        s[0].revalidateField("MainCategory");
                    }),
                    $(i.querySelector('[name="SubCategory"]')).on("change", function () {

                        s[0].revalidateField("SubCategory");
                    }),

                    $(i.querySelector('[name="Area"]')).on("change", function () {
                        s[1].revalidateField("Area");
                    }),
                    $(i.querySelectorAll('[name="dayslot"]')).on("change", function () {
                        console.log("Dayslot radio button changed");
                        s[1].revalidateField("dayslot");
                    }),
                    $(i.querySelectorAll('[name="frequency"]')).on("change", function () {
                        console.log("Freq");
                        alert(2);
                        s[1].revalidateField("frequency");
                    })
                );

            // Use standard event listener for DOMContentLoaded to ensure everything is loaded before attaching event listeners
            document.addEventListener("DOMContentLoaded", function () {
                console.log("DOM fully loaded and parsed");
                attachRadioListeners();
            });
        },
    };
})();
KTUtil.onDOMContentLoaded(function () {
    KTCreateAccount.init();
});
