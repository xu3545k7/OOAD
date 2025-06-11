<template>
    <div>
        <h2>登入</h2>
        <form @submit.prevent="login">
            <label for="username">用戶名:</label>
            <input id="username" v-model="username" required />

            <label for="password">密碼:</label>
            <input id="password" v-model="password" type="password" required />

            <button type="submit">登入</button>
        </form>
        <p v-if="message">{{ message }}</p>
    </div>
</template>

<script>
export default {
    data() {
        return {
            username: "",
            password: "",
            message: "",
        };
    },
    methods: {
        async login() {
            try {
                const response = await fetch("http://localhost:5164/api/account/login", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        username: this.username,
                        password: this.password,
                    }),
                });

                if (!response.ok) {
                    const errorData = await response.json();
                    throw new Error(errorData.message || "登入失敗");
                }

                this.message = "登入成功！";
            } catch (error) {
                this.message = error.message;
            }
        },
    },
};
</script>
