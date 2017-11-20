(function () {
    var app = new Vue({
        created: function () {
            Vue.nextTick(function() {
                $('#datePicker').datepicker({
                    autoclose: true,
                    minViewMode: 1,
                    format: 'yyyy-mm',
                    language: 'sv',
                    endDate: new Date().toJSON().slice(0, 10)
                }).show();
            });

        },
        data: function () {
            return {
                user: {}
            }
        },
        methods: {
            getUserInfo: function () {
                const self = this;
                $.ajax({
                    url: apiUrl + "User",
                    type: "GET",
                    success: function (response) {
                        if (response) {
                            self.user = response;
                        }
                    },
                    error: function (error) {

                    }
                });
            }
        }
    }).$mount("#createMonthlyBudgetApp");
})();