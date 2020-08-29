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
            for (let key in this.options) {
                this.select2data.push(
                    { id: this.options[key].id, text: this.options[key].name, name: "abc" })
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