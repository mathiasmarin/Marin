(function () {
    var app = new Vue({
        created: function () {
            const self = this;
            self.getAllCategories();
        },
        data: function () {
            return {
                savedCategories: [],
                categories: [],
                categoryName: ""
            }
        },
        methods: {
            addCategory: function () {
                const self = this;
                self.categories.push(self.categoryName);
                self.categoryName = "";
            },
            removeCategory: function (index) {
                const self = this;
                self.categories.splice(index, 1);

            },
            deleteCategory: function (id, index) {
                const self = this;
                var msg = '<div class="row text-center"><i class="fa fa-exclamation-triangle fa-5x text-danger"></i></div><div class="row text-center">Vil du ta bort denna kategori?</div>';
                bootbox.confirm({
                    title: '<div class="row text-center">Varning</div>',
                    message: msg,
                    buttons: {
                        cancel: {
                            label: '<i class="fa fa-times"></i> Avbryt',
                            className: 'btn-danger'
                        },
                        confirm: {
                            label: '<i class="fa fa-check"></i> Ta bort',
                            className: 'btn-success'
                        }
                    },
                    callback: function (result) {
                        if (result) {
                            $.ajax({
                                url: apiUrl + "Budget/Category/" + id,
                                type: "DELETE",
                                success: function (response) {
                                    self.savedCategories.splice(index, 1);
                                    CreateConfirmModal("Kategorin togs bort");

                                },
                                error: function (error) {
                                    CreateErrorModal(error.responseText);
                                }
                            });
                        }
                    }
                });

            },
            saveCategories: function () {
                const self = this;
                $.ajax({
                    url: apiUrl + "Budget/Categories",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({
                        Categories: self.categories
                    }),
                    success: function (response) {
                        CreateConfirmModal("Kategorier sparades");
                        self.categories = [];
                        self.getAllCategories();
                    },
                    error: function (error) {
                        CreateErrorModal(error.responseText);

                    }
                });
            },
            getAllCategories: function () {
                const self = this;
                $.ajax({
                    url: apiUrl + "Budget/Categories",
                    type: "GET",
                    success: function (response) {
                        if (response) {
                            self.savedCategories = response;
                        }
                    },
                    error: function (error) {
                        CreateErrorModal(error.responseText);
                    }
                });
            }
        }
    }).$mount("#createBudgetApp");
})();