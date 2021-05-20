import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserServiceService } from '../services/user-service.service';
import { User} from 'src/app/model/user';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

    registerationForm!:FormGroup;
    user!:User;
    userSubmitted:boolean=false;

    constructor(private fb:FormBuilder,
                private userService:UserServiceService,
                private alertify:AlertifyService) { }

    ngOnInit(): void {
      // this.registerationForm=new FormGroup({
      //   userName:new FormControl("Dev",Validators.required),
      //   email:new FormControl(null,[Validators.required,Validators.email]),
      //   password:new FormControl(null,[Validators.required,Validators.minLength(8) ]),
      //   confirmPassword:new FormControl(null,Validators.required ),
      //   mobile:new FormControl(null,[Validators.required,Validators.maxLength(10)])
      // },  { validators: this.passwordMatchingValidator });
      this.createRegisterationForm();
    }

    createRegisterationForm()
    {
      this.registerationForm=this.fb.group({
       // userName:['Dev',Validators.required],
       userName:[null,Validators.required],
        email:[null,[Validators.required,Validators.email]],
        password:[null,[Validators.required,Validators.minLength(8)]],
        confirmPassword:[null,Validators.required],
        mobile:[null,[Validators.required,Validators.maxLength(10)]]
      },{validators:this.passwordMatchingValidator})
    }

    passwordMatchingValidator(fg:AbstractControl): Validators|null{
       return fg.get('password')?.value===fg.get('confirmPassword')?.value ? null :
       {notMathed:true};
    }
    //getter method for registrationForm controls
    get userName(){
      return this.registerationForm.get('userName') as FormControl
    }

    get email(){
      return this.registerationForm.get('email') as FormControl
    }

    get password(){
      return this.registerationForm.get('password') as FormControl
    }

    get confirmPassword(){
      return this.registerationForm.get('confirmPassword') as FormControl
    }
    get mobile(){
      return this.registerationForm.get('mobile') as FormControl
    }
    // emailMatchValidator(control: AbstractControl) {
    //   if (control.value !== 'emailaddress@gmail.com') {
    //     return false;
    //   } else {
    //     return { emailExists: true };
    //   }
    // }

    onSubmit(){
      this.userSubmitted=true;
      if(this.registerationForm.valid){
        console.log(this.registerationForm);
     // this.user=Object.assign(this.user,this.registerationForm.value);
        //localStorage.setItem('Users',JSON.stringify(this.user));
        //this.userService.addUser(this.user);
        this.userService.addUser(this.userData());
        this.registerationForm.reset();
        this.userSubmitted=false;
        this.alertify.success("Congrats! you have successfully registered.");
      }
      else{
        this.alertify.error("Please fill in the required values.");
      }
    }

    userData():User{
      return this.user={
        userName:this.userName.value,
        email:this.email.value,
        password:this.password.value,
        mobile:this.mobile.value
      }
    }
    //moved below addUser function to the user-service.service.ts file
    // addUser(user:any={}){
    //   let users=[];

    //   if(localStorage.getItem('Users')){
    //     users=JSON.parse(localStorage.getItem('Users')||'{}');
    //     users=[user, ...users];
    //   }
    //   else{
    //     users=[user];
    //   }
    //   localStorage.setItem('Users',JSON.stringify(users));
    // }

}
