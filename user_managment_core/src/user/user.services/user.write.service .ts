import { Inject, Injectable } from '@nestjs/common';
import { IredisReadService, IredisWriteService } from 'src/rediscache/contracrs';
import { EventEmitter2 } from '@nestjs/event-emitter';

import * as bcrypt from 'bcrypt';
import { IuserCommandRepository, IuserReadService, IuserWriteService } from '../contracts/user.repository.Interface';
@Injectable()
export class UserWriteService implements IuserWriteService {

   constructor(@Inject('IuserCommandRepository') private readonly userrepositoryinterface: IuserCommandRepository,
      @Inject('IredisReadService') private readonly redisReadService: IredisReadService,
      @Inject("IuserReadService") private readonly useReadService: IuserReadService,
      private eventEmitter: EventEmitter2) { }


   async registerService(body: any): Promise<any> {

      const resUserForRedis= await this.redisReadService.redisfindUser(body.email);
   
      console.log(resUserForRedis)
     if(resUserForRedis){
         return "user is already exist"
     }
   
  
   //   const resUser= await this.useReadService.getProfileByEmailService(body.email);
   //     if(!resUser){
   //        return "user is already exist"}
        
      const { password } = body;
     
      const hashedPassword = await bcrypt.hash(password, 10);
      body.password = hashedPassword;
    

      const res = await this.userrepositoryinterface.register(body);
      if (res) {
         console.log('Emitting user.created.event');
       
         await this.eventEmitter.emit('user.created.event', body );
     }
   //    return res; 
   }

   async updateUserService(id: string, updateRegistracionDto: any): Promise<any> {
      const res = await this.userrepositoryinterface.updateUser(id, updateRegistracionDto);
      if (res) {
         this.eventEmitter.emit('user.update.event', updateRegistracionDto);
      }
      return res;
   }

   async deleteByemailService(email: string): Promise<any> {
      const res = await this.userrepositoryinterface.deleteByemail(email);
      if (res) { this.eventEmitter.emit('user.delete.event', email); }
   }






}
