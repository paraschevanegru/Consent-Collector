import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { combineAll } from 'rxjs/operators';
import { Notifications } from 'src/app/models/notification';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  public notifications:Notifications[]=[];
  public cloneNotifications:Notifications[]=[];
  @Output() notificationEvent = new EventEmitter<number>();
  constructor(private readonly profileService:ProfileService) { }

  ngOnInit(): void {
    this.profileService.GetAllNotificationOfUser(this.profileService.currentIdUser).subscribe(
      (data)=>{
        this.notifications=data;
        this.cloneNotifications=data;
        console.log("notificari:",this.notifications)
      }
    )
  }
  public sendNotification(idNotification?:string):void{
    this.cloneNotifications.forEach((el,index)=>{
      if(idNotification==el.id){
        var notification=new Notifications(el.title,el.description, el.idUser, el.idSurvey,true);
        notification.id=idNotification;
        this.profileService.ReadNotification(idNotification??"null",notification).subscribe(
          (data)=>{
            this.notifications = this.notifications.filter(item => item.id!=idNotification);
            console.log("new not:",this.notifications)
          }
        )
      }
    })
    this.notificationEvent.emit(1);
  }

}
