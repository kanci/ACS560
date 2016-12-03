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
	"crypto/md5"
)

const (
    	DB_USER     = "postgres"
    	DB_PASSWORD = "123456"
    	DB_NAME     = "test"
)

func convert( b [16]byte ) string {
    	s := make([]string,len(b))
    	for i := range b {
		i64 := int64(int(b[i]))
        	s[i] = strconv.FormatInt(i64, 16)
    	}
    	return strings.Join(s,"")
}

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

func getMD5(inString string) string {
        data := []byte(inString)
        return convert(md5.Sum(data))
}

func closeDBCconnection(datastuffs *sql.DB) {
	datastuffs.Close()
}

func createDBtable(datastuffs *sql.DB) {
        datastuffs.Close()
}

func adduser(datastuffs *sql.DB, name string, password string) error{
	var userinfo string
	var MD5Password string
	MD5Password = getMD5(name+""+password)
	userinfo += "(name, password, score) VALUES ('" + name + "', '" + MD5Password + "', 0);"
	fmt.Println(userinfo)
	err1 := insertDBrow(datastuffs, "users", userinfo)
	return err1
}


func insertDBrow(datastuffs *sql.DB, table string, condtion string) error{
        query := "INSERT INTO "
        query += table +" " + condtion
        _ , err1 := datastuffs.Query(query)
        checkErr(err1)
        datastuffs.Close()
        return err1
}

func existsDBrow(datastuffs *sql.DB, attribute string, table string, condtion string) error{
        query := "SELECT  "
        query += attribute + " FROM "+table +" " + condtion
        _ , err1 := datastuffs.Query(query)
        return err1
}


func ReadDBtable(datastuffs *sql.DB, attribute string, table string, condtion string) (string,error){
        var value string
	query := "SELECT "
	query += attribute + " FROM " + table +" " + condtion
	

        rows1, err1 := datastuffs.Query(query)
        for rows1.Next() {
                err1 = rows1.Scan(&value)
                checkErr(err1)
        }

	datastuffs.Close()
	return value, err1
}

func updateDBrow(datastuffs *sql.DB, attribute string, invalue string, table string, condtion string) {

        var value string
        query := "UPDATE "
        query += table +" SET " + attribute + "=" + invalue + condtion

        rows1, err1 := datastuffs.Query(query)
        checkErr(err1)

        for rows1.Next() {
                err1 = rows1.Scan(&value)
                checkErr(err1)
        }

        datastuffs.Close()


}

func deleteDBrow(datastuffs *sql.DB) {

        datastuffs.Close()
}


func userExists(datastuffs *sql.DB, login string) bool{
	
	if (existsDBrow(datastuffs , "name", "users", "where name = '"+login+"';")==nil) {
	return true
	} else {
	fmt.Println("userExists user: "+login+" not found")
	return false
	}
}


func  pullboard(datastuffs *sql.DB, boardnumber int) [64]int {
        var value string
	condtion := "where id = " + strconv.Itoa(boardnumber)
	value, err1 := ReadDBtable(datastuffs, "value", "boards", condtion)
	checkErr(err1)
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
        //panic(err)
	fmt.Println("base error has occoured")
    	}
}


func validateUser(datastuffs *sql.DB, Player player) bool {
        var MD5return string
        condtion := "where name = '"+Player.getPlayerLogin()+"';"
	MD5return = ""
	fmt.Println("trying user: " + Player.getPlayerLogin())
	fmt.Println("MD5sum: "+getMD5(Player.getPlayerLogin()+""+Player.getPlayerPassword()))
	fmt.Println(MD5return)
	if (userExists(datastuffs, Player.getPlayerLogin())) {
		fmt.Println("validateing user: ")
        	MD5return, _ := ReadDBtable(datastuffs, "password", "users", condtion)
		fmt.Println(MD5return)	
		if(MD5return == getMD5(Player.getPlayerLogin()+""+Player.getPlayerPassword())) {
			return true
		} else {
			return false
		}

	} else {
		return false
	}
}


func loginServer(w http.ResponseWriter, r *http.Request) {

	newPlayer := player{Login: r.URL.Query()["user"][0], Password: r.URL.Query()["pass"][0]}

	if (validateUser(databaseConnect(), newPlayer)) {
        	var newBoard = pullboard(databaseConnect(),1)
		newPlayer := player{Login: r.URL.Query()["user"][0], Password: r.URL.Query()["pass"][0], Score: 0, Board: newBoard}
		json.NewEncoder(w).Encode(newPlayer)
	} else {
		json.NewEncoder(w).Encode("error, user not found")
	}
}

func createUser(w http.ResponseWriter, r *http.Request) {


        if (adduser(databaseConnect(), r.URL.Query()["user"][0], r.URL.Query()["pass"][0])==nil) {
        	io.WriteString(w, "createing user")
		newPlayer := player{Login: r.URL.Query()["user"][0], Password: r.URL.Query()["pass"][0]}
		fmt.Println("creating user")
		fmt.Println("login: ", newPlayer.getPlayerLogin())
		fmt.Println("password: ", newPlayer.getPlayerPassword())
		fmt.Println()
	} else {
	        io.WriteString(w, "error createing user, user already exists")
	}
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
