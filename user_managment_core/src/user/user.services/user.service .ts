import { Inject, Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { RedisRepository } from 'src/rediscache/rediscache.RedisRepository';
import { Userrepositoryinterface } from '../user.repository/contracts/user.repository.Interface';

@Injectable()
export class UserService {

    constructor(@Inject('Userrepositoryinterface') private readonly redisService: Userrepositoryinterface) { }




}
