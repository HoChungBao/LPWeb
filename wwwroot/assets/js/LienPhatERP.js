
Vue.component('select-2', {
    template: '<select v-bind:name="name"  v-bind:multiple="multiple" v-bind:placeholder="placeholder" style="width:100%"></select>',
    props: {
        name: '',
        options: {
            Object
        },
        value: null,
        multiple: {
            Boolean,
            default: false

        },
        placeholder: ''
    },
    data() {
        return {
            select2data: []
        }
    },
    mounted() {
        this.formatOptions();
        let vm = this;
        let select = $(this.$el);
        select
            .select2({
                placeholder: this.placeholder,
                //theme: 'bootstrap',
                width: '100%',
                data: this.select2data
            })
            .on('change', function () {
                vm.$emit('input', select.val())
            })
        select.val(this.value).trigger('change')
    },
    methods: {
        formatOptions() {
            if (this.name == "test") {
                for (let key in this.options) {
                    this.select2data.push(
                        { id: key, text: this.options[key].name})
                }

            }            
        },
    },
    watch: {
        options: function (options) {
            this.formatOptions();
            this.select2data.unshift({ id: "", text: "Tất cả" });
            $(this.$el).empty().select2({ data: this.select2data });
        }
    },
    destroyed: function () {
        $(this.$el).off().select2('destroy')
    },
});
const singleSelect = Vue.component('single-select', {
    data() {
        return {
            selected1: '',
            options: ''
        }
    }
});

var app = new Vue({
    el: '#app',
    data: {
        ArrayCity: '',
        ArrayDistrict: '',
        ArrayDistrict2:'',
        selectDistrict: '',
        selectDistrict2:'',
        selectCity: '',
        Roles: '',
        Roles1: '',
        selectedRole: '',
        selected: [],
        location: window.location.origin,
        selectName: '',
        selectEmail: '',
        selectContact: '',
        selectAddress: '',
        selectTotalLed: '',
        selectDay: '',
        selectWeek: '',
        selectPriceMonopoly: '',                       
        selectHospital:'1',               
        selectSpecia: '',
        selectSpecia2:'',
        selectProduct: '',
        selectDescriptionPoint: '',
        selectLinkProfile: '',
        selectAmount: '',
        selectAmount2: '',
        Hospital:'',
        Products: '',
        Pharmacy:'',
        Specias: '',
        Specias2: '',
        file: [],
        dataCsCreens: '',
        display: true,
        DetailHospital: [],
        DetailPharmacy: []
                       
        
        //page: window.location.pathname.split("/")[3].toLowerCase(),
    },
    //hàm lấy dữ liệu khi khỏi tạo trang web
    created: function () {
        this.GetRoles();
        this.GetProductSpecia();
        this.GetProvince();
    },
    methods: {
        GetRoles: function () {
            var self = this;
            axios.get(this.location + '/Account/DataJsonRoles').then(function (response) {
                self.Roles1 = response.data.result;                
            }, function (response) {
            });
        },
        //Hàm Get Province
        GetProvince: function () {
            var self = this;
            axios.get(this.location + '/Common/Province').then(function (response) {
                self.ArrayCity = response.data.provinces;                              
            }, function (response) {
            });
        },
        //hàm lấy dữ liệu Product & Specia
        GetProductSpecia: function () {            
            var self = this;           
            axios.get(this.location + '/Common/GetDataJsonCommon').then(function (response) {
                self.Products = response.data.commonJson.productTypes;              
                self.Specias = response.data.commonJson.speciaList;
                self.Specias2 = response.data.commonJson.speciaList;
                
            }, function (response) {
            });
        },
        //hàm AddRole
        AddRoles: function () {
            var self = this;
            axios({
                'method': 'post', 'url': this.location +'/Account/AddRole', params: { role: self.selectedRole }
            }).then(function (response) {
                self.Roles = response.data.Redirect;
                if (response != null) {
                    swal("Thành Công", response.data.Message, "success");
                    //self.GetRoles();
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                }                              
            }, function (response) {
                alert("Đã xãy ra lỗi");
             })
        }, 
        //hàm remove Role
        RemoveRole: function (role) {
            var self = this;
            axios({
                'method': 'get', 'url': this.location + '/Account/RemoveRole', params: { Id: role.id }
            }).then(function (response) {               
                if (response != null) {
                    if (response.data.result) {
                        swal(response.data.message, "", "success");
                        self.GetRoles();
                    }
                    else
                        swal(response.data.message, "", "warning");
                   
                }
                
            }, function (response) {
                alert("Đã xãy ra lỗi");
                });
            
        },
        //getItemAirContent: function (CsCreens) {
        //    alert(CsCreens);
        //    var self = this;
        //    self.dataCsCreens = CsCreens;
        //    this.display = true;
        //},
        ImageLink: function (event) {
            var seft = this;
            for (var i = 0; i < event.target.files.length; i++) {
                this.file.push(event.target.files[i]);
            }           
        },
        //Hàm Insert New CsCreens
        InsertCsCreens: function ()
        {
            var self = this;
            console.log(self);
            var Array = [];
            if (self.selectProduct!= null) {
                self.selectProduct.forEach(function (v) {
                    Array.push(self.Products[v].name);
                })
                var StringProduct = Array.join();
            }
            axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';
            const formData = new FormData();
            this.file.forEach(function (v) {
                formData.append("Files", v);
            });
            
            
            axios({
                'method': 'post', 'url': this.location + '/Common/InsertCsCreen', 'data': formData, params: {
                    Speciality: self.selectSpecia,                   
                    Name: self.selectName,
                    Email: self.selectEmail,
                    Phone: self.selectContact,
                    Address: self.selectAddress + ", " + self.selectDistrict.text + ", " + self.selectCity.text,
                    TotalLed: self.selectTotalLed,
                    TrafficDaily: self.selectDay,
                    PriceWeekly: self.selectWeek,
                    PriceMonopoly: self.selectPriceMonopoly,
                    IsHospital: self.selectHospital === "1" ? true : false,
                    AdvertiseProductsType: StringProduct,                    
                    Province: self.selectCity.id,
                    District: self.selectDistrict.id,
                    DescriptionPoint: self.selectDescriptionPoint,
                    LinkProfile: self.selectLinkProfile,
                    AmountScreenDetails: JSON.stringify(self.selectHospital=="1" ? self.DetailHospital : self.DetailPharmacy)
                }                                   
            }).then(function (response) {
                if (response != null) {
                    swal("Thành Công", response.data.Message, "success");
                    setTimeout(function () {                       
                        location.reload();
                    }, 2000);
                }

                    },function (response) {
                        alert("Đã xãy ra lỗi");
            })
        },
        Details: function () {
            if (this.selectHospital == "1") {
                var Hospital = {
                    Name: "",
                    Amount:""
                }
                Hospital.Name = this.selectSpecia2;
                Hospital.Amount = this.selectAmount;
                this.DetailHospital.push(Hospital);
            }
            else {
                var Pharmacy = {                  
                    District: "",
                    Amount: ""
                }
                Pharmacy.District = this.selectDistrict2.id;
                Pharmacy.Amount = this.selectAmount2;
                this.DetailPharmacy.push(Pharmacy);
            }
        },
        
        //Hàm Checkall Lây Id
        onCheckAll: function (event) {
            this.checkAll = !this.checkAll;
            this.selected = [];
            if (this.checkAll) {
                var that = this;
                this.Roles1.forEach(function (Role) {
                    that.selected.push(Role.id);
                })
                return;
            }
        },
        //Hàm fillter Province hiển trị District
        filterDictrict: function () {
            this.ArrayDistrict = this.selectCity.districts;
            this.selectDistrict = this.ArrayDistrict[0];
            this.ArrayDistrict2 = this.selectCity.districts;         
            this.selectDistrict2 = this.ArrayDistrict2;

        }
    },
    watch: {
        selected: function (newSelected) {
            if (this.checkAll && newSelected.length !== this.Roles1.length) {
                this.checkAll = false;
            }
        }
    }
            
    });
const options = app.Products;