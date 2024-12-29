import { Module } from '@nestjs/common';
import { GetdataService } from './getdata.service';
import { GetdataController } from './getdata.controller';

@Module({
  controllers: [GetdataController],
  providers: [GetdataService],
})
export class GetdataModule {}
