(function () {
    var app = new Vue({
        created: function () {
            const self = this;
        },
        data: function () {
            return {
                categories: [],
                categoryName: ""
            }
        },
        methods: {
            addCategory: function() {
                const self = this;
                self.categories.push(self.categoryName);
                self.categoryName = "";
            },
            removeCategory: function(index) {
                const self = this;
                self.categories.splice(index, 1);

            },
            saveCategories: function() {
                const self = this;
                $.ajax({
                    url: apiUrl + "Budget/Categories",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({
                        Categories: self.categories
                    }),
                    success: function (response) {
                        if (response) {
                            CreateConfirmModal("Kategorier sparades");
                        }
                    },
                    error: function (error) {

                    }
                });
            }
        }
    }).$mount("#createBudgetApp");
})();