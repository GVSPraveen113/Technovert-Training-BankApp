#include <iostream>
#include <string>
#include <cstring>
#include <map>
#include <vector>

using namespace std;


class Poll{
    public:
    int optionSize;
    int totalVotesSoFar=0;
    
    map<string,int> mp;
    
    void createPoll(int n,vector<string> options){
        this->optionSize=n;
        for(int i=0;i<optionSize;i++){
            mp[options[i]]=0;
        }
    }

    
    void submitPoll(int i, vector<string> options){
        mp[options[i]]++;
        totalVotesSoFar++;
        
    }
    void viewResults(){
        cout<<endl;
        cout<<"Poll Results:"<<endl;
        for(auto m:mp){
            cout<<m.first<<" "<<m.second*100/totalVotesSoFar<<"% ("<<m.second<<" votes)"<<endl;
        }
    
    }
};

int main() {
    Poll newPoll;
    cout<<"Enter optionsize and options"<<endl;
    int n;
    cin>>n;
    
    string s;
    vector<string> v;

    for(int i=0;i<n;i++){
        cin>>s;
        v.push_back(s);
    }
    newPoll.createPoll(n,v);

    int numberOfGroupMembers;
    cin>>numberOfGroupMembers;

    cout<<"Select option selected by everyone";
    int x;
    for(int i=0;i<numberOfGroupMembers;i++){
        cin>>x;
        newPoll.submitPoll(x,v);
    }
    newPoll.viewResults();
    return 0;
}
