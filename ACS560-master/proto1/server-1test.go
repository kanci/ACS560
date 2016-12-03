package main

import (
	"fmt"
	"io"
	"net/http"
	"strconv"	
    	"database/sql"
    	_ "github.com/lib/pq"
 	"encoding/json"
	"strings"
)

const (
    	DB_USER     = "postgres"
    	DB_PASSWORD = "123456"
    	DB_NAME     = "test"
)

type player struct{
	Login, Password string
	Score int 
	Board [64]int
}

func (p *player) getPlayerLogin() string {
	return p.Login
}

func (p *player) getPlayerPassword() string {
	return p.Password
}

func (p *player) getPlayerScore() int {
	return p.Score
}

func (p *player) getBoard() [64]int {
	return p.Board
}


type board struct{
	candy []string
}

func closeDBCconnection(datastuffs *sql.DB) {
	datastuffs.Close()
}

func createDBtable(datastuffs *sql.DB) {

        datastuffs.Close()
}

func instertDBrow(datastuffs *sql.DB) {

        datastuffs.Close()
}

func ReadDBtable(datastuffs *sql.DB, attribute string, table string) {
        var value string
	query := "SELECT "
	query += attribute + " FROM " + table
	
        rows1, err1 := datastuffs.Query(query)
        checkErr(err1)

        for rows1.Next() {
                err1 = rows1.Scan(&value)
                checkErr(err1)
        }

	datastuffs.Close()
}

func updateDBrow(datastuffs *sql.DB) {

        datastuffs.Close()
}

func deleteDBrow(datastuffs *sql.DB) {

        datastuffs.Close()
}




func  pullboard(datastuffs *sql.DB) [64]int {
        var value string
	rows1, err1 := datastuffs.Query("SELECT value FROM boards where id=1")
    	checkErr(err1)

    	for rows1.Next() {
        	err1 = rows1.Scan(&value)
        	checkErr(err1)
	}

	var someboard = [64] int {}
	var dummyint64 int64
   	boardvalues := strings.Split(value, ",")

    	// assign all elements.
    	for i := range boardvalues {
		dummyint64, err1 = strconv.ParseInt(boardvalues[i], 10, 32)
		someboard[i] = int(dummyint64)
    	}

	closeDBCconnection(datastuffs)
	return someboard
}

func databaseConnect() *sql.DB {
    	dbinfo := fmt.Sprintf("user=%s password=%s dbname=%s sslmode=disable",DB_USER, DB_PASSWORD, DB_NAME)
    	db, err := sql.Open("postgres", dbinfo)
    	checkErr(err)
	return db
}

func checkErr(err error) {
    	if err != nil {
        	panic(err)
	fmt.Println("error has occoured")
    	}
}

func listenServer(w http.ResponseWriter, r *http.Request) {
	io.WriteString(w, "t")
}

func loginServer(w http.ResponseWriter, r *http.Request) {
        var newBoard = pullboard(databaseConnect())
	newPlayer := player{Login: r.URL.Query()["user"][0], Password: r.URL.Query()["pass"][0], Score: 0, Board: newBoard}
	json.NewEncoder(w).Encode(newPlayer)
}

func createUser(w http.ResponseWriter, r *http.Request) {
	io.WriteString(w, "createing user")
	newPlayer := player{Login: r.URL.Query()["user"][0], Password: r.URL.Query()["pass"][0]}
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
