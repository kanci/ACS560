package main

import (
	"fmt"
	"io"
	"net/http"
//	"strconv"	
)







type player struct{
login, password string
score int 
//board []board
}

func (p *player) getPlayerLogin() string {
return p.login
}

func (p *player) getPlayerPassword() string {
return p.password
}

func (p *player) getPlayerScore() int {
return p.score
}




type board struct{
candy []string
}






func listenServer(w http.ResponseWriter, r *http.Request) {
io.WriteString(w, "t")
}

func loginServer(w http.ResponseWriter, r *http.Request) {
//io.WriteString(w, "ok, login?")
newPlayer := player{login: r.URL.Query()["user"][0], password: r.URL.Query()["pass"][0]}
fmt.Println("login: ", newPlayer.getPlayerLogin())
fmt.Println("password: ", newPlayer.getPlayerPassword())
io.WriteString(w, "login\nuser: ")
io.WriteString(w, newPlayer.getPlayerLogin())
io.WriteString(w, "\npassword: ")
io.WriteString(w, newPlayer.getPlayerPassword())
}

func createUser(w http.ResponseWriter, r *http.Request) {
io.WriteString(w, "createing user")
newPlayer := player{login: r.URL.Query()["user"][0], password: r.URL.Query()["pass"][0]}
fmt.Println("login: ", newPlayer.getPlayerLogin())
fmt.Println("password: ", newPlayer.getPlayerPassword())


}

func getNextBoard(w http.ResponseWriter, r *http.Request) {

i := r.URL.Query()["board"][0]
next := "nextBoard: " + i
io.WriteString(w, next)

fmt.Println("current: ", i)
fmt.Println("next: ", i)
}






func main() {



http.HandleFunc("/login", loginServer)
http.HandleFunc("/new", createUser)
http.HandleFunc("/nextBoard", getNextBoard)
http.ListenAndServe(":80", nil)

}
