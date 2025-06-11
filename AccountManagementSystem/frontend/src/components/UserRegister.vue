<template>
    <div>
        <h2>註冊</h2>
        <form @submit.prevent="register">
            <label for="username">用戶名:</label>
            <input id="username" v-model="username" required />

            <label for="email">電子郵件:</label>
            <input id="email" v-model="email" type="email" required />

            <label for="password">密碼:</label>
            <input id="password" v-model="password" type="password" required />

            <button type="submit">註冊</button>
        </form>
        <p v-if="message">{{ message }}</p>
    </div>
</template>

<script>
export default {
    data() {
        return {
            username: "",
            email: "",
            password: "",
            message: "",
        };
    },
    methods: {
        async register() {
            try {
                const response = await fetch("http://localhost:5164/api/account/register", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        username: this.username,
                        email: this.email,
                        password: this.password,
                        Role: "PM", // 預設角色為 PM
                    }),
                });

                if (!response.ok) {
                    // 嘗試解析錯誤訊息，如果失敗就用文字內容或預設訊息
                    let errorMessage = "註冊失敗";
                    try {
                        const errorData = await response.json();
                        errorMessage = errorData.message || errorMessage;
                    } catch {
                        // 無法解析成 JSON，改用純文字
                        const text = await response.text();
                        errorMessage = text || errorMessage;
                    }
                    throw new Error(errorMessage);
                }

                this.message = "註冊成功！";
                this.username = this.email = this.password = "";
            } catch (error) {
                this.message = error.message;
            }
        },
    },
};
</script>
