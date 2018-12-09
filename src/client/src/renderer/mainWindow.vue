<template>
    <div>
        <div class="message" v-for="message in messages" :key="message.id">
            {{ message.content }}
        </div>
        <input class="messageInput" type="text" placeholder="Начните воодить сообщение..." v-model="newMessage" @keyup.enter="sendMessage">
    </div>
</template>


<script>
    export default {
        data() {
            return {
                newMessage: '',
                idForMessage: 3,
                messages: [
                    {
                        'id': 1,
                        'content': 'fist message'
                    },
                    {
                        'id': 2,
                        'content': 'second message'
                    }
                ]
            }
        },
        methods: {
            sendMessage() {
                this.messages.push({
                    id: this.idForMessage,
                    content: this.newMessage
                })
                this.newMessage = ''
                this.idForMessage++
            }
        }
    }
</script>

<style lang="scss">
@import '/scss/style.scss'
</style>

