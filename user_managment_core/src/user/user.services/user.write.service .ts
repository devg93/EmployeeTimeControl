import { Inject, Injectable } from '@nestjs/common';
import { IredisServiceInterface } from 'src/rediscache/contracrs';
import { EventEmitter2 } from '@nestjs/event-emitter';
import { IuserWriteInterface, Userrepositoryinterface } from '../user.repository/contracts/user.repository.Interface';

@Injectable()
export class UserWriteService implements Partial<IuserWriteInterface> {

   constructor(@Inject('Userrepositoryinterface') private readonly userrepositoryinterface: Userrepositoryinterface,
      @Inject('IredisServiceInterface') private readonly redisService: IredisServiceInterface,
      private eventEmitter: EventEmitter2) { }

   async register(body: any): Promise<any> {
      const res = await this.userrepositoryinterface.register(body);
      if (res) //return await this.redisService.redisregisterUser(body);
      {
         return this.eventEmitter.emit('user.created.event', body);
      }
   }

   async updateUser(id: string, updateRegistracionDto: any): Promise<any> {
      const res = await this.userrepositoryinterface.updateUser(id, updateRegistracionDto);
      if (res) {
         // await this.redisService.redisupdateUser(id, updateRegistracionDto);
         this.eventEmitter.emit('user.update.event', updateRegistracionDto);
      }
      return res;

   }

   async deleteByemail(email: string): Promise<any> {
      const res = await this.userrepositoryinterface.deleteByemail(email);
      if (res) //await this.redisService.redisdelete(email);
      { this.eventEmitter.emit('user.delete.event', email); }
   }


}
