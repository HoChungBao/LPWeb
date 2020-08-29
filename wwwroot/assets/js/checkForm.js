function checkInput1() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            noteCustomer: {
                required: true
            },
            note: {
                required: true
            },

            //== Validation messages
            messages: {
                'account_communication[]': {
                    required: 'You must select at least one communication option'
                },
                accept: {
                    required: "You must accept the Terms and Conditions agreement!"
                }
            },

            //== Display error
            invalidHandler: function (event, validator) {


                swal({
                    "title": "",
                    "text": "There are some errors in your submission. Please correct them.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            },

            //== Submit valid form
            submitHandler: function (form) {

            }
        }
    });
    return {
        result: validator.form()
    };
};
function checkInput() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            customerName: {
                required: true
            },
            //type: {
            //    required: true,
            //},
            //== Mailing addres
            Email: {
                required: true
            },
            Phone: {
                required: true
            },
            Contact: {
                required: true
            },
            customerPIC: {
                required: true
            },
            customerPosition: {
                required: true
            },
        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "There are some errors in your submission. Please correct them.",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};
function checkInputContract(value) {
    //== Base elements
    var formEl = value;
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            NoContract: {
                required: true
            },
            Name: {
                required: true
            },
            StartDate: {
                required: true
            },
            EndDate: {
                required: true
            },
            TotalMoney: {
                required: true
            },
            namePayment: {
                required: true
            },
            TotalMoneyPayment: {
                required: true
            },
            DatePayment: {
                required: true
            },
        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "There are some errors in your submission. Please correct them.",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};
function checkInputAirContent(value) {
    //== Base elements
    var formEl = value;
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details           
            Name: {
                required: true
            },
            StrStartDate: {
                required: true
            },
            StrEndDate: {
                required: true
            },
            Durations: {
                required: true
            },
            typeAirContent: {
                required: true
            },
            Status: {
                required: true
            },
        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "There are some errors in your submission. Please correct them.",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};

function checkInputContact() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            name: {
                required: true
            },
            linkimage: {
                required: true
            },

            category: {
                required: true
            },
            Email: {
                required: true
            },
            Address: {
                required: true
            },
            Phone: {
                required: true
            }

        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "Có lỗi xãy ra, Vui lòng bổ sung lại thông tin ",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};

function checkInputProduct() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            name: {
                required: true
            },         
            ShortDescription: {
                required: true
            },
            Description: {
                required: true
            },
            Price: {
                required: true
            }          



        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "Có lỗi xãy ra, Vui lòng bổ sung lại thông tin ",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};

function checkInputMediHub() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            CsScreenId: {
                required: true
            },
            Address: {
                required: true
            },

            Tvbrand: {
                required: true
            },
            Tvsize: {
                required: true
            },
            Seri: {
                required: true
            },
            Box: {
                required: true
            },
            Timer: {
                required: true
            },
            Specicality: {
                required: true
            }            



        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "Có lỗi xãy ra, Vui lòng bổ sung lại thông tin ",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};

function checkInputBusinessTrip() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            Name: {
                required: true
            },
            Timer: {
                required: true
            },

            Address: {
                required: true
            },
            Customer: {
                required: true
            },
            Contact: {
                required: true
            },
            Phone: {
                required: true
            }

        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "Có lỗi xãy ra, Vui lòng bổ sung lại thông tin ",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};
function checkInputDsPlayer() {
    //== Base elements
    var formEl = $('#m_form');
    var validator;
    validator = formEl.validate({
        //== Validate only visible fields
        ignore: ":hidden",

        //== Validation rules
        rules: {
            //=== Client Information(step 1)
            //== Client details
            PlayerId: {
                required: true
            },
            PlayerName: {
                required: true
            },

            Address: {
                required: true
            }
        },

        //== Validation messages
        messages: {
            'account_communication[]': {
                required: 'You must select at least one communication option'
            },
            accept: {
                required: "You must accept the Terms and Conditions agreement!"
            }
        },

        //== Display error
        invalidHandler: function (event, validator) {


            swal({
                "title": "",
                "text": "Có lỗi xãy ra, Vui lòng bổ sung lại thông tin ",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },

        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    return {
        result: validator.form()
    };
};