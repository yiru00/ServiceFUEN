export default {
  methods: {
    showAlert(message) {
      this.$swal.fire({
        text: message,
        toast: true,
        position: "bottom",
        showConfirmButton: false,
        showCancelButton: false,
        background: "#555",
        color: "#fff",
        timer: 800,
        width: "fit-content",
        padding: "5px",
      });
    },
    async fetchMemberId() {
      let Id = 0;
      let token = $.cookie("token");
      let options = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      };
      try {
        let response = await fetch(
          "https://localhost:7259/api/Members/Read",
          options
        );
        let data = await response.json();

        Id = data;
        console.log("登入成功" + Id);
        return Id;
      } catch (error) {
        console.log("未登入");

        return Id;
      }
    },
    scrollToTop() {
      window.scrollTo({
        top: 0,
        behavior: "smooth", // 平滑滚动
      });
    },
  },
};
