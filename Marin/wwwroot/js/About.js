(function () {
    var app = new Vue({
        created: function() {
            const self = this;
            self.getUserInfo();
        },
        data: function() {
            return {
                user: {}
            }
        },
        methods: {
            getUserInfo: function() {
                const self = this;
                $.ajax({
                    url: apiUrl + "User",
                    type: "GET",
                    success: function(response) {
                        if (response) {
                            self.user = response;
                        }
                    },
                    error: function(error) {

                    }
                });
            }
        }
    }).$mount("#aboutPage");
})();