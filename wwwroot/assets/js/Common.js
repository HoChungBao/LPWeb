var app = new Vue({
    el: '#app',
    data: {
        ArrayProvince: '',
        ArrayDistrict: '',
        selectProvince: '',
        selectDistrict: '',
        ArrayDistrict2: '',
        selectProvince2: -1,
        selectDistrict2: -1,
        FilterPharmacy: '',
        Hospitals: [],
        Pharmacies: [],
        isHospital: '',
        Specials1: '',
        selectSpecias1: 0,
        location: window.location.origin,
        global: [],
        txtHospital: '',
        txtPharmacy: '',
        selectProduct: [],
        Products: [],
        checkDocument: '',
        check: "",
        documnetRequiedTypeProduct: "",
        disabledproduct: "",
        checkedHospital: [],
        checkedPharmacy: [],
        selected:[],
    },
    created: function() {
        this.GetProvince();
        this.Get();
        this.GetProductSpecia();
    },
    methods: {
        GetProvince: function() {
            var self = this;
            axios.get(this.location + '/Common/Province').then(function(response) {
                    self.ArrayProvince = response.data.provinces;

                },
                function(response) {
                });
        },
        GetProductSpecia: function() {
            var self = this;
            axios.get(this.location + '/Common/GetDataJsonCommon').then(function(response) {
                    self.Products = response.data.commonJson.productTypes;

                },
                function(response) {
                });
        },
        //lấy dữ liệu bệnh viện theo bệnh viện, nhà thuốc theo nhà thuốc
        Get: function() {
            var self = this;
            var tempHos = [];
            var tempPhar = [];
            axios.get(this.location + '/Common/GetDataJsonCommon').then(function(response) {
                    global = response.data.commonJson.hospitalName;
                    global.forEach(function(v) {
                        if (v.isHospital == true) {
                            tempHos.push(v);
                        } else {
                            tempPhar.push(v);
                        }
                    });
                    self.Hospitals = tempHos;
                    self.Pharmacies = tempPhar;
                    self.Specials1 = response.data.commonJson.speciaList;
                },
                function(response) {
                });
        },

        filterDistrict: function() {
            this.ArrayDistrict2 = this.selectProvince2.districts;
        },
        District: function() {
            this.ArrayDistrict = this.selectProvince.districts;
            //this.selectDistrict = this.ArrayDistrict[0];
        },
        disablevalue: function (hospital) {
            var that = this;
            if (hospital.advertiseProductsType== null) return false;
            var h = hospital.advertiseProductsType.split(",");
            for (var i = 0; i < h.length; i++) {
                if (that.disabledproduct == h[i])
                    return true;
            }
            return false;
        },
        getHospital: function () {
            return this.checkedHospital;
        },
        getPharmacy: function () {
            return this.checkedPharmacy;
        },
        //checkValue: function (array, id) {
        //    //alert(array);     
        //    //for (var i = 0; i < array.length; i++) {
        //    //    if (array[i] == id)
        //    //        return true;
        //    //}
        //    //return false;
        //    this.checkedHospital = array;
        //},
        uploadFiles: function (event) {
            event.preventDefault();
            var stepsArray = [];
            var steps = [];

            this.pressCodeTypeProduct.map(function(unit, index) {
                stepsArray.push(index + 1);
                steps.push({
                    title: unit.Name
                });
            });

            swal.setDefaults({
                input: 'file',
                //html:
                //'<div class="custom-file"><input type="file" class="custom-file-input swal2-input" id="fileDoc">' +
                //    ' <label for="fileDoc" class="custom-file-label">  Chọn file </label ></div >' ,
                //input:
                 width: 600,
                padding: 20,
                inputClass: "form-control",
                buttonClass: "form-control",
                confirmButtonText: 'Tiếp theo &rarr;',
                cancelButtonText:'Hủy',
                showCancelButton: true,
                progressSteps: stepsArray
            });

            swal.queue(steps).then((result) => {
                swal.resetDefaults();

                var isNotNull = result.value.every(function(value) {
                    return value;
                });

                if (!isNotNull) {
                    swal({
                        type: 'error',
                        title: 'Oops...',
                        text: "Xin hãy upload đầy đủ " + result.value.length + " files"
                    });
                    return;
                }

                if (result.value) {
                    var data = new FormData();

                    for (var x = 0; x < result.value.length; x++) {
                        data.append("files", result.value[x]);
                    }

                    $.ajax({
                        type: "POST",
                        url: "/Common/UploadFile",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (respone) {
                            if (respone != null && respone.data != '') {
                                    isUploadContract = true;
                                    $('#RelatedDocuments').val(respone.data);
                                    swal("Upload Giấy tờ liên quan", "Thành công", "success");
                            }

                        },
                        error: function () {
                            window.alert("Upload Error!!");
                        }
                    });
                }
            });
        },
        SearchFilter: function (keyword, list) {
            var filter = new RegExp(keyword, 'i');
            return list.filter(function (item) {
                return item.name.match(filter);
            });
        },
    },

    computed: {
        hosFilter: function () {
            var that = this;
            //alert(this.selectSpecias1);
            if (!this.selectSpecias1) {

                if (!this.txtHospital) {
                    return this.Hospitals;
                }

                var filter = new RegExp(this.txtHospital, 'i');
                return this.Hospitals.filter(function(item) {

                    return item.name.match(filter);
                });
            } else {
                if (this.selectSpecias1 === '0') {
                    return this.Hospitals;
                    var filter = new RegExp(this.txtHospital, 'i');
                    return tempHos.filter(function (item) {
                        return item.name.match(filter);
                    });
                }
                var tempHos = this.Hospitals.filter(function (item) {
                    if (item.amountScreenDetails) {
                        var filter = new RegExp(that.selectSpecias1, 'i'); //dùng RegExp để filter
                        var subSpec = JSON.parse(item.amountScreenDetails);// khai báo biến Sub để parse 
                        var totalSpeciasItem = item.speciality + subSpec.reduce(function (sum, item) { //tạo biến total để kết ghép  amountScreenDetails và speciality khi filter
                            return sum + item.Name;
                        }, "");       
                        //console.log(JSON.stringify(filter.test(totalSpeciasItem)));
                        return filter.test(totalSpeciasItem);
                    }
                    return item.speciality === that.selectSpecias1;
                });

                if (this.txtHospital) tempHos = this.SearchFilter(this.txtHospital, tempHos);

                tempHos = tempHos.map(function (hos) {
                    var newHos = Object.assign({}, hos);
                    if (newHos.speciality !== that.selectSpecias1) {
                        var amountArray = JSON.parse(newHos.amountScreenDetails);
                        amountArray.forEach(function (amount) {
                            if (amount.Name === that.selectSpecias1) {
                                newHos.totalLed = Number(amount.Amount);
                            }
                        });
                    }               
                    return newHos;
                });

                return tempHos;
            }
        },

        //Hàm fillter textbox, 2 cái select
        PharFilter: function() {
            var that = this;
            var pharmacies = this.Pharmacies;

            if (this.selectProvince2 !== -1) {
                pharmacies = this.Pharmacies.filter(function(item) { //ngược lại trả ra province = select lấy theo id
                    return item.province == that.selectProvince2.id;
                });
            }

            if (this.selectDistrict2 !== -1) {
                pharmacies = pharmacies.filter(function(item) {
                    return item.district === that.selectDistrict2.id;
                });
            }

            if (this.txtPharmacy) {
                var filter = new RegExp(this.txtPharmacy, 'i');
                pharmacies = pharmacies.filter(function(item) {
                    return item.name.match(filter);
                });
            }
            return pharmacies;
        },
        pressCodeTypeProduct: function () {
            if (!this.selectProduct) return [];
            var that = this;
            var product = this.Products.filter(function(item) {
                if (that.selectProduct == item.id) {
                    return true;
                }
                return false;
            });
            if (product.length > 0) {
                this.disabledproduct = product[0].name;
                this.documnetRequiedTypeProduct = JSON.parse(product[0].documentrequired);
                return JSON.parse(product[0].pressCode); //.pressCode
            }
            return [];
        },
        //documnetRequiedTypeProduct: function() {
            //if (!this.checkDocument) return [];

        //    var that = this;
        //    var product = this.Products.filter(function(item) {
        //        if (that.checkDocument.indexOf(item.id + "") > -1) {
        //            return true;
        //        }
        //        return false;
        //    });
        //    if (product.length > 0) return JSON.parse(product[0].pressCode).SampleDoc;
        //    return [];
        //    //}).map(function (item) {
        //    //    return item.pressCode.sampleDoc;
        //    //});
        //},
        docmentRequiedProductType: function() {

            if (that.checkDocument = true) return JSON.parse(pressCode.SampleDoc);
        },
        selectlicense: function (value) {
            alert(value);
        },
        selectedAllHospital: {
            get: function () {
                return this.hosFilter ? this.checkedHospital.length == this.hosFilter.length : false;
            },
            set: function (value) {
                var checkedHospital = [];

                if (value) {
                    this.hosFilter.forEach(function (Hospital) {
                        checkedHospital.push(Hospital.id);
                    });
                }

                this.checkedHospital = checkedHospital;
            }
        },
        selectedPharmacy: {
            get: function () {
                return this.PharFilter ? this.checkedPharmacy.length == this.PharFilter.length : false;
            },
            set: function (value) {
                var checkedPharmacy = [];

                if (value) {
                    this.PharFilter.forEach(function (Pharmacy) {
                        checkedPharmacy.push(Pharmacy.id);
                    });
                }

                this.checkedPharmacy = checkedPharmacy;
            }
        },
    },    
    //Hàm fillter select options
        //FilterSpecial: function () {
        //    var self = this;

        //    self.Hospitals = [];
        //    global.forEach(function (v) {
        //        if (v.speciality == self.selectSpecias1 && v.isHospital == true) {
        //            self.Hospitals.push(v);
        //        }
        //    });
        //},
});