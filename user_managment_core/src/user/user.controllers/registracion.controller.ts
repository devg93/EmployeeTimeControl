import { Controller, Get, Post, Body, Patch, Param, Delete } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { RegistracionRepository } from '../user.repository.services/regiRepository.service';
import { ApiOperation } from '@nestjs/swagger';
import { RedisService } from '../services/redis.service';



@Controller('registracion')
export class RegistracionController {
  constructor(private readonly registracionService: RegistracionRepository,private readonly redisService:RedisService) {}

  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {
    
    await this.redisService.registerUser(body); //redis 
  
    return   await this.registracionService.register(body);

  }
  @Get()
  async findAll() {  
    return await this.registracionService.findAll();
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
     await this.redisService.findUser(id) //redis 
    return await this.registracionService.findOneUser(+id);
  }

  @Patch(':id')
  async update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    await this.redisService.updateUser(id,updateRegistracionDto);
    return await this.registracionService.updateUser(+id, updateRegistracionDto);
  }

  @Delete(':id')
async  remove(@Param('id') id: string) {
    return await this.registracionService.remove(+id);
  }
}
